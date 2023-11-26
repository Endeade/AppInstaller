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
using System.Runtime.InteropServices;
using static AppInstaller.EffectBlur;
using AutoUpdaterDotNET;

namespace AppInstaller
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        private uint _blurOpacity;
        public double BlurOpacity
        {
            get { return _blurOpacity; }
            set { _blurOpacity = (uint)value; EnableBlur(); }
        }

        private uint _blurBackgroundColor = 0x990000;

        internal void EnableBlur()
        {
            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
            accent.GradientColor = (_blurOpacity << 24) | (_blurBackgroundColor & 0xFFFFFF);
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;
            SetWindowCompositionAttribute(this.Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }

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
            var process = Process.Start(downloadpath, "/qn");
            process.WaitForExit();
            progressBar1.Visible = false;
            button1.Text = "Installed";
            wait(5000);
            button1.Text = "Update";
            button5.Visible = true;
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

            }
            else
            {
                Directory.CreateDirectory(appinstallerfiles);
            }
            progressBar1.Visible = false;

            if (checkBox2.Checked)
            {
                if (checkBox2.Checked == true)
                {
                    AutoUpdater.Start("https://raw.githubusercontent.com/Endeade/endeade.github.io/main/appinstaller/autoupdater.xml");

                }
            }
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
            var process = Process.Start(downloadpath,"/qn");
            process.WaitForExit();
            progressBar1.Visible = false;
            button1.Text = "Installed";
            wait(5000);
            button1.Text = "Update";
            button7.Visible = true;
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
            var process = Process.Start(downloadpath, "/qn");
            process.WaitForExit();
            progressBar1.Visible = false;
            button3.Text = "Installed";
            wait(5000);
            button3.Text = "Update";
            button8.Visible = true;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\Vivaldi.exe";
            progressBar1.Visible = true;
            button4.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://downloads.vivaldi.com/stable/Vivaldi.6.1.3035.111.x64.exe"), downloadpath);
            button4.Text = "Installing...";
            var process = Process.Start(downloadpath, "--vivaldi-silent --do-not-launch-chrome --system-level");
            process.WaitForExit();
            progressBar1.Visible = false;
            button4.Text = "Installed";
            wait(5000);
            button4.Text = "Update";
            button9.Visible = true;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\Discord.exe";
            progressBar1.Visible = true;
            button6.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://dl.discordapp.net/distro/app/stable/win/x86/1.0.9015/DiscordSetup.exe"), downloadpath);
            button6.Text = "Installing...";
            var process = Process.Start(downloadpath, "-s");
            process.WaitForExit();
            progressBar1.Visible = false;
            button6.Text = "Installed";
            wait(5000);
            button6.Text = "Update";
        }


        private async void button12_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\Telegram.exe";
            progressBar1.Visible = true;
            button12.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://updates.tdesktop.com/tx64/tsetup-x64.4.8.3.exe"), downloadpath);
            button12.Text = "Installing...";
            var process = Process.Start(downloadpath, "-s");
            process.WaitForExit();
            progressBar1.Visible = false;
            button12.Text = "Installed";
            wait(5000);
            button12.Text = "Update";
            button11.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string uninst = GetEnvironmentVariable("localappdata") + "\\Discord\\Update.exe";
            button10.Text = "Uninstalling...";
            var process = Process.Start(uninst, "--uninstall -s");
            process.WaitForExit();
            button10.Text = "Uninstalled";
            wait(5000);
            button10.Visible = false;
            button6.Text = "Install";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string uninst = GetEnvironmentVariable("PROGRAMFILES(X86)") + "\\Microsoft\\Edge\\Application\\";
            string[] dirs = Directory.GetDirectories(uninst, "*", SearchOption.AllDirectories);
            string uninstdir = dirs[0];
            string uninstaller = uninstdir + "setup.exe";
            button5.Text = "Uninstalling...";
            var process = Process.Start(uninstaller, "-uninstall -system-level –verbose-logging –force-uninstall");
            process.WaitForExit();
            button5.Text = "Uninstalled";
            wait(5000);
            button5.Visible = false;
            button1.Text = "Install";
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\Element.exe";
            progressBar1.Visible = true;
            button16.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://packages.riot.im/desktop/install/win32/ia32/Element%20Setup.exe"), downloadpath);
            button16.Text = "Installing...";
            var process = Process.Start(downloadpath, "-s");
            process.WaitForExit();
            progressBar1.Visible = false;
            button16.Text = "Installed";
            wait(5000);
            button16.Text = "Update";
            button15.Visible = true;
        }   

        private void button11_Click(object sender, EventArgs e)
        {
            string uninst = GetEnvironmentVariable("appdata") + "\\Telegram Desktop\\unins000.exe";
            button11.Text = "Uninstalling...";
            var process = Process.Start(uninst, "/VERYSILENT /NORESTART");
            process.WaitForExit();
            button11.Text = "Uninstalled";
            wait(5000);
            button11.Visible = false;
            button12.Text = "Install";
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\Revolt.exe";
            progressBar1.Visible = true;
            button14.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://github.com/revoltchat/desktop/releases/download/v1.0.6/Revolt-Setup-1.0.6.exe"), downloadpath);
            button14.Text = "Installing...";
            var process = Process.Start(downloadpath, "-s");
            process.WaitForExit();
            progressBar1.Visible = false;
            button14.Text = "Installed";
            wait(5000);
            button14.Text = "Update";
            button13.Visible = true;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //Revolt uninstall
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Chrome uninstall
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Firefox uninstall
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Vivaldi uninstall
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Element uninstall
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string message = "Acrylic Mode is an experimental feature. Are you sure you want to enable it?";
                string title = "AppInstaller - Experimental Feature";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.BackColor = System.Drawing.ColorTranslator.FromHtml("#010000");
                    Settings.BackColor = System.Drawing.ColorTranslator.FromHtml("#010000");
                    Utilities.BackColor = System.Drawing.ColorTranslator.FromHtml("#010000");
                    Browsers.BackColor = System.Drawing.ColorTranslator.FromHtml("#010000");
                    ChatApps.BackColor = System.Drawing.ColorTranslator.FromHtml("#010000");
                    TabControl.BackColor = System.Drawing.ColorTranslator.FromHtml("#010000");
                    Gaming.BackColor = System.Drawing.ColorTranslator.FromHtml("#010000");
                    EnableBlur();
                    label1.ForeColor = Color.White;
                    label2.ForeColor = Color.White;
                    label3.ForeColor = Color.White;
                    label4.ForeColor = Color.White;
                    label5.ForeColor = Color.White;
                    label6.ForeColor = Color.White;
                    label7.ForeColor = Color.White;
                    label8.ForeColor = Color.White;
                    label9.ForeColor = Color.White;
                    label10.ForeColor = Color.White;
                    label11.ForeColor = Color.White;
                    label12.ForeColor = Color.White;
                    label13.ForeColor = Color.White;
                    label14.ForeColor = Color.White;
                    label15.ForeColor = Color.White;
                    checkBox1.ForeColor = Color.White;
                    checkBox2.ForeColor = Color.White;
                }
                else
                {
                    checkBox1.Checked = false;
                }

            }
            else
            {
                Application.Restart();
                Exit(0);
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void button24_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\SteamSetup.exe";
            progressBar1.Visible = true;
            button24.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://cdn.akamai.steamstatic.com/client/installer/SteamSetup.exe"), downloadpath);
            button24.Text = "Installing...";
            var process = Process.Start(downloadpath, "/S");
            process.WaitForExit();
            progressBar1.Visible = false;
            button24.Text = "Installed";
            wait(5000);
            button24.Text = "Update";
            button23.Visible = true;

        }

        private void button23_Click(object sender, EventArgs e)
        {
            // only works on x64 systems, needs fix
            string uninst = GetEnvironmentVariable("ProgramFiles(x86)") + "\\Steam\\uninstall.exe";  
            button23.Text = "Uninstalling...";
            var process = Process.Start(uninst, "/S");
            process.WaitForExit();
            button23.Text = "Uninstalled";
            wait(5000);
            button23.Visible = false;
            button21.Text = "Install";
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\GOGGalaxy.exe";
            progressBar1.Visible = true;
            button22.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://webinstallers.gog-statics.com/download/GOG_Galaxy_2.0.exe"), downloadpath);
            button22.Text = "Installing...";
            var process = Process.Start(downloadpath, "/S");
            process.WaitForExit();
            progressBar1.Visible = false;
            button22.Text = "Installed";
            wait(5000);
            button22.Text = "Update";
            button21.Visible = true;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            // GOG Galaxy uninstall
        }

        private async void button20_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\EpicGamesLauncher.msi";
            progressBar1.Visible = true;
            button20.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://launcher-public-service-prod06.ol.epicgames.com/launcher/api/installer/download/EpicGamesLauncherInstaller.msi"), downloadpath);
            button20.Text = "Installing...";
            var process = Process.Start(downloadpath, "/qn");
            process.WaitForExit();
            progressBar1.Visible = false;
            button20.Text = "Installed";
            wait(5000);
            button20.Text = "Update";
            button19.Visible = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            // Epic Games uninstaller
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string downloadpath = appinstallerfiles + "\\PrismLauncher.exe";
            progressBar1.Visible = true;
            button22.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://github.com/PrismLauncher/PrismLauncher/releases/download/7.1/PrismLauncher-Windows-MSVC-Setup-7.1.exe"), downloadpath);
            button22.Text = "Installing...";
            var process = Process.Start(downloadpath, "/qn");
            process.WaitForExit();
            progressBar1.Visible = false;
            button22.Text = "Installed";
            wait(5000);
            button22.Text = "Update";
            button21.Visible = true;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // Prism Launcher uninstaller
        }
    }
}
