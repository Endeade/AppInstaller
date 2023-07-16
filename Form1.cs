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

        private void button1_Click(object sender, EventArgs c)
        {
            string downloadpath = appinstallerfiles + "\\MSEdgeSetup.msi";
            button1.Text = "Installing...";
            progressBar1.Visible = true;
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(new Uri("https://msedge.sf.dl.delivery.mp.microsoft.com/filestreamingservice/files/e25e63b7-e29e-4c86-bd45-e7d683b9a2bd/MicrosoftEdgeEnterpriseX64.msi"), downloadpath);
                }
                catch
                {
                    MessageBox.Show("FAIL!");
                }
            }
            progressBar1.Visible = false;
            Process.Start(downloadpath + " /qb");
            button1.Text = "Installed";
            wait(5000);
            button1.Text = "Install";
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            string downloadpath = appinstallerfiles + "\\Chrome.msi";
            button2.Text = "Installing...";
            using (var client = new WebClient())
            {
                client.DownloadFile("https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7BA2AFEE09-00D4-4A6E-BFAB-365F04535F02%7D%26lang%3Den%26browser%3D5%26usagestats%3D0%26appname%3DGoogle%2520Chrome%26needsadmin%3Dtrue%26ap%3Dx64-stable-statsdef_0%26brand%3DGCEA/dl/chrome/install/googlechromestandaloneenterprise64.msi", downloadpath);
            }
            Process.Start(downloadpath, "/q");
            button2.Text = "Installed";
            wait(5000);
            button2.Text = "Install";
            
        }
    }
}
