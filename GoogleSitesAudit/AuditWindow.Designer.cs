namespace GoogleSitesAudit
{
    partial class AuditWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGo = new System.Windows.Forms.Button();
            this.tbDomain = new System.Windows.Forms.TextBox();
            this.dgvSites = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMaxResults = new System.Windows.Forms.TextBox();
            this.cbAllResults = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSites)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(568, 10);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // tbDomain
            // 
            this.tbDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDomain.Location = new System.Drawing.Point(61, 12);
            this.tbDomain.Name = "tbDomain";
            this.tbDomain.Size = new System.Drawing.Size(205, 20);
            this.tbDomain.TabIndex = 0;
            // 
            // dgvSites
            // 
            this.dgvSites.AllowUserToAddRows = false;
            this.dgvSites.AllowUserToDeleteRows = false;
            this.dgvSites.AllowUserToResizeRows = false;
            this.dgvSites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSites.Location = new System.Drawing.Point(12, 38);
            this.dgvSites.Name = "dgvSites";
            this.dgvSites.ReadOnly = true;
            this.dgvSites.Size = new System.Drawing.Size(631, 418);
            this.dgvSites.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Domain";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(343, 462);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 469);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 13);
            this.lblProgress.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Max Results";
            // 
            // tbMaxResults
            // 
            this.tbMaxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMaxResults.Location = new System.Drawing.Point(343, 12);
            this.tbMaxResults.Name = "tbMaxResults";
            this.tbMaxResults.Size = new System.Drawing.Size(100, 20);
            this.tbMaxResults.TabIndex = 1;
            this.tbMaxResults.Text = "250";
            // 
            // cbAllResults
            // 
            this.cbAllResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAllResults.AutoSize = true;
            this.cbAllResults.Checked = true;
            this.cbAllResults.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllResults.Location = new System.Drawing.Point(449, 14);
            this.cbAllResults.Name = "cbAllResults";
            this.cbAllResults.Size = new System.Drawing.Size(113, 17);
            this.cbAllResults.TabIndex = 8;
            this.cbAllResults.Text = "Include All Results";
            this.cbAllResults.UseVisualStyleBackColor = true;
            // 
            // AuditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 497);
            this.Controls.Add(this.cbAllResults);
            this.Controls.Add(this.tbMaxResults);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSites);
            this.Controls.Add(this.tbDomain);
            this.Controls.Add(this.btnGo);
            this.Name = "AuditWindow";
            this.Text = "Google Sites Audit";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSites)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox tbDomain;
        private System.Windows.Forms.DataGridView dgvSites;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMaxResults;
        private System.Windows.Forms.CheckBox cbAllResults;
    }
}

