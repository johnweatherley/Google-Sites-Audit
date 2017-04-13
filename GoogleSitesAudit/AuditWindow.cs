using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GoogleSitesAudit
{
    public partial class AuditWindow : Form
    {        
        private static string[] scopes = { "https://sites.google.com/feeds" };
        private string token;

        private List<GoogleSite> sites = new List<GoogleSite>();

        public AuditWindow()
        {
            InitializeComponent();
            
            //setup dgv
            ContextMenuStrip dgvCM = new ContextMenuStrip();
            var save = new ToolStripMenuItem("Save");
            save.Click += new EventHandler(ExportData_Click);
            dgvCM.Items.Add(save);
            dgvSites.ContextMenuStrip = dgvCM;

            dgvSites.AutoGenerateColumns = true;
            dgvSites.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSites.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Authorize using OAuth2, this will pop open a browser window if you haven't already
            //authorized the app.  If you haven't provided the JSON secret, you'll need to go and do that first.
            UserCredential credential;
            
            if (!File.Exists("client_secret.json"))
            {
                if (MessageBox.Show("Credential JSON file not found.  Please create the proper credentials at https://console.developers.google.com/projectselector/apis/credentials and save the JSON file to the program directory.  Go there now?", "JSON Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes) { System.Diagnostics.Process.Start("https://console.developers.google.com/projectselector/apis/credentials"); }
                tbDomain.Enabled = false;
                tbMaxResults.Enabled = false;
                btnGo.Enabled = false;
                cbAllResults.Enabled = false;

                Application.Exit();
                Environment.Exit(0);
            }

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/google-sites-audit.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            //set the token
            token = credential.Token.AccessToken;            
        }

        /// <summary>
        /// Export the DataGridView to CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportData_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "CSV|*.csv";
            saveDialog.Title = "Save to CSV";
            
            var sb = new StringBuilder();

            var headers = dgvSites.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            foreach (DataGridViewRow row in dgvSites.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
            }

            saveDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveDialog.FileName))
            {
                var fs = saveDialog.OpenFile();
                var file = new StreamWriter(fs);
                file.Write(sb.ToString());
                file.Close();
                fs.Close();
            }
        }

        /// <summary>
        /// Start the Audit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGo_Click(object sender, EventArgs e)
        {
            //clear the sites list
            sites = new List<GoogleSite>();

            //setup the progress bar
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;

            var progress = new Progress<int>(v =>
            {
                progressBar1.Value = v;
            });

            btnGo.Enabled = false;

            lblProgress.Text = "Running...";

            //async run the getsites function
            await Task.Run(() => GetSites(progress));

            lblProgress.Text = string.Format("{0} sites found from {1}", sites.Count, tbDomain.Text);
            progressBar1.Value = 0;
            btnGo.Enabled = true;

            //bind sites to the dgv
            var bindingList = new SortableBindingList<GoogleSite>(sites);
            var source = new BindingSource(bindingList, null);
            
            dgvSites.DataSource = source;
        }

        private void GetSites(IProgress<int> progress)
        {
            //put together the request to the api.
            var requestString = string.Format(@"https://sites.google.com/feeds/site/{0}?include-all-sites={1}&max-results={2}", tbDomain.Text, cbAllResults.Checked.ToString().ToLower(), tbMaxResults.Text);
            var request = (HttpWebRequest)WebRequest.Create(requestString);
            request.Method = "GET";
            request.Headers.Add("GData-Version", "1.4");
            request.Headers.Add("Authorization", "Bearer " + token);

            HttpWebResponse response;
            
            //try and get the response, error if something goes wrong.
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("401"))
                {
                    string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    credPath = Path.Combine(credPath, ".credentials\\google-sites-audit.json");

                    MessageBox.Show("Unauthorized request, your token has probably expired.  Delete the token and re-authorize.  The token is located at " + credPath, "401", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(e.Message);
                }

                sites = new List<GoogleSite>();
                return;
            }

            try
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var xdoc = XDocument.Load(sr);

                    var entries = xdoc.Descendants().Where(d => d.Name.LocalName == "entry").ToList(); //turn this into a direct select into object later.

                    for (int i = 0; i<entries.Count(); i++)
                    {
                        var entry = entries[i];

                        var id = entry.Descendants().Where(d => d.Name.LocalName == "id").First().Value;
                        var title = entry.Descendants().Where(d => d.Name.LocalName == "title").First().Value;
                        var updated = entry.Descendants().Where(d => d.Name.LocalName == "updated").First().Value;
                        var summary = entry.Descendants().Where(d => d.Name.LocalName == "summary").First().Value;

                        id = id.Replace("https://sites.google.com/feeds/site/" + tbDomain.Text, "");

                        var lastActivity = GetLastActivity(id);
                        var owners = GetOwners(id);

                        //add google site
                        sites.Add(new GoogleSite(id, title, updated, summary, lastActivity, owners));
                        
                        //update the progress bar
                        if (progress != null)
                        {
                            progress.Report(Convert.ToInt32(((double)i / entries.Count()) * 100));                            
                        }
                    }
                }
            }
            catch
            {
                sites = new List<GoogleSite>();
                return;
            }
        }

        private string GetLastActivity(string site)
        {
            //build the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://sites.google.com/feeds/activity/" + tbDomain.Text + site);
            request.Method = "GET";
            request.Headers.Add("GData-Version", "1.4");
            request.Headers.Add("Authorization", "Bearer " + token);

            //get the response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string retval;
            try
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var xdoc = XDocument.Load(sr);

                    var entry = xdoc.Descendants().Where(d => d.Name.LocalName == "entry").First();
                    var date = entry.Descendants().Where(d => d.Name.LocalName == "updated").First().Value;
                                       
                    retval = date;
                }
            }
            catch
            {
                return null;
            }

            return retval;
        }

        private List<string> GetOwners(string site)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://sites.google.com/feeds/acl/site/" + tbDomain.Text + site);
            request.Method = "GET";
            request.Headers.Add("GData-Version", "1.4");
            request.Headers.Add("Authorization", "Bearer " + token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            List<string> owners = new List<string>();

            try
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var xdoc = XDocument.Load(sr);

                    var entries = xdoc.Descendants().Where(d => d.Name.LocalName == "entry");

                    foreach (var entry in entries)
                    {
                        var user = entry.Descendants().Where(d => d.Name.LocalName == "scope").First().Attribute("value").Value;
                        var role = entry.Descendants().Where(d => d.Name.LocalName == "role").First().Attribute("value").Value;

                        if (role == "owner") owners.Add(user);
                    }
                }
            }
            catch
            {
                return new List<string>() { "Error" };
            }

            return owners;
        }
    }

    /// <summary>
    /// Google Site audit object
    /// </summary>
    class GoogleSite
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? Updated { get; set; }
        public string Summary { get; set; }
        public DateTime? LastActivity { get; set; }
        public List<string> Owners { get; set; }
        public string OwnersList
        {
            get
            {
                return string.Join("; ", Owners);
            }
        }

        public GoogleSite() { }

        public GoogleSite(string id, string title, string updated, string summary, string lastActivity, List<string> owners)
        {
            Id = id;
            Title = title;
            Summary = summary;            
            Owners = owners;

            if (string.IsNullOrEmpty(updated)) { Updated = null; } else { Updated = DateTime.Parse(updated); }
            if (string.IsNullOrEmpty(lastActivity)) { LastActivity = null; } else { LastActivity = DateTime.Parse(lastActivity); }
        }
        public GoogleSite(string id, string title, DateTime? updated, string summary, DateTime? lastActivity, List<string> owners)
        {
            Id = id;
            Title = title;
            Updated = updated;
            Summary = summary;
            LastActivity = lastActivity;
            Owners = owners;
        }
    }
}
