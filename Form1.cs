using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using static System.Environment;
using static System.Uri;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace AppInstaller
{
    public partial class Form1 : Form
    {
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        string appinstallerfiles = GetEnvironmentVariable("userprofile") + "\\Downloads\\appinstallerfiles";
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs c)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\Edge.msi";
            progressBar1.Visible = true;
            button1.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://msedge.sf.dl.delivery.mp.microsoft.com/filestreamingservice/files/e25e63b7-e29e-4c86-bd45-e7d683b9a2bd/MicrosoftEdgeEnterpriseX64.msi"), downloadpath);
            button1.Text = "Installing...";
            var process = Process.Start(downloadpath);
            process.WaitForExit();
            progressBar1.Visible = false;
            button1.Text = "Installed";
            wait(5000);
            button1.Text = "Install";
        }
        
        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        } 

        private void Form1_Load(object sender, EventArgs e)
        {
            string appinstallerfiles = Environment.GetEnvironmentVariable("userprofile") + "\\Downloads\\appinstallerfiles";
            if (Directory.Exists(appinstallerfiles))
            {

            } else
            {
                Directory.CreateDirectory(appinstallerfiles);
            }
            progressBar1.Visible = false;
        }

       

        private async void button2_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\Chrome.msi";
            progressBar1.Visible = true;
            button1.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7BA2AFEE09-00D4-4A6E-BFAB-365F04535F02%7D%26lang%3Den%26browser%3D5%26usagestats%3D0%26appname%3DGoogle%2520Chrome%26needsadmin%3Dtrue%26ap%3Dx64-stable-statsdef_0%26brand%3DGCEA/dl/chrome/install/googlechromestandaloneenterprise64.msi"), downloadpath);
            button1.Text = "Installing...";
            var process = Process.Start(downloadpath);
            process.WaitForExit();
            progressBar1.Visible = false;
            button1.Text = "Installed";
            wait(5000);
            button1.Text = "Install";
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\Firefox.msi";
            progressBar1.Visible = true;
            button3.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://download.mozilla.org/?product=firefox-msi-latest-ssl&os=win64&lang=en-US&_gl=1*1r8hyod*_ga*MjAzODg4MzkwNi4xNjg5NTg5MzMx*_ga_MQ7767QQQW*MTY4OTU4OTMzMC4xLjEuMTY4OTU4OTM2OS4wLjAuMA.."), downloadpath);
            button3.Text = "Installing...";
            var process = Process.Start(downloadpath);
            process.WaitForExit();
            progressBar1.Visible = false;
            button3.Text = "Installed";
            wait(5000);
            button3.Text = "Install";
        }
    }
}
