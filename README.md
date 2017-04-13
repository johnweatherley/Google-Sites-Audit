# Google-Sites-Audit
Tool to audit Google Sites

This tool can be used to retreive the ID (URL), Title, Summary, Last Updated Date, Last Activity Date, and Owners for all google sites.

To use the API, you'll need to create and download a credential JSON file from here: https://console.developers.google.com/projectselector/apis/credentials
Move the credential file to the program directory and rename it to "client_secret.json"
After you do this, you'll be able to run the tool and Goolge will ask you to authorize the API credentials.

There are three user inputs:

Domain - Text.  If your Google Sites URL starts with https://sites.google.com/a/domain.com/ then 'domain.com' is what you enter.
Max Results - Numeric.  The number of sites you want to return.  Default is 250, if you have more, then enter a higher number.
Include All Results - Boolean.  If True, return all sites your user has access to, if False, return only the user's sites.

Once sites are returned, you can right click on the data grid view and save as a CSV.