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
using AutoUpdaterDotNET;
using Microsoft.Win32;
using KPreisser.UI;
using static System.Windows.Forms.LinkLabel;

namespace AppInstaller
{
    public partial class Form1 : Form
    {

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
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
        RegistryKey appinstsetting = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\\AppInstallerSettings");
        private void Form1_Closed(object sender, FormClosedEventArgs e)
        {

            if (appinstsetting != null)
            {
                if (checkBox1.Checked == true)
                {
                    appinstsetting.SetValue("AutoUpdate", 1);
                }
                else
                {
                    appinstsetting.SetValue("AutoUpdate", 0);
                }

                if (checkBox2.Checked == true)
                {
                    appinstsetting.SetValue("DarkMode", 1);
                }
                else
                {
                    appinstsetting.SetValue("DarkMode", 0);
                }
            }

        }

        // all of this winget shit is FUCKING BROKEN!
        // IT IS A TESTING BUILD
        // i am aware of all the fucking bugs with it
        // calm your asses down while i fucking fix them up

        // istfg visual studio if i get ANOTHER FUCKING 
        // WARNING ABOUT ME NOT USING AWAIT OPERATORS
        // I WILL BURN THE MS HEADQUARTERS

        private async void button1_Click(object sender, EventArgs c)
        {
            progressBar1.Visible = true;
            progressBar1.Value = 50;
            button1.Text = "Installing through winget...";
            var process = Process.Start("cmd", "/c winget install --id Microsoft.Edge");
            process.WaitForExit();
            progressBar1.Value = 100;
            wait(1000);
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
            KPreisser.UI.TaskDialog td = new();
            td.Page.Text = "This is a beta build of AppInstaller";
            td.Page.Instruction = "If you do run into bugs, please report them.";
            td.Page.Title = "AppInstaller";
            td.Page.StandardButtons = TaskDialogButtons.OK;
            td.Page.Icon = TaskDialogStandardIcon.SecurityWarningYellowBar;
            td.Page.EnableHyperlinks = true;
            KPreisser.UI.TaskDialogExpander tde = new();
            tde.Text = "You can launch, but the experience will be buggy";
            tde.Expanded = true;
            tde.ExpandFooterArea = true;
            tde.CollapsedButtonText = "More info";
            tde.ExpandedButtonText = "Less info";
            td.Page.Expander = tde;
            td.Show();
            string appinstallerfiles = Environment.GetEnvironmentVariable("userprofile") + "\\Downloads\\appinstallerfiles";
            if (Directory.Exists(appinstallerfiles))
            {

            }
            else
            {
                Directory.CreateDirectory(appinstallerfiles);
            }
            progressBar1.Visible = false;


            if (appinstsetting != null)
            {
                if (appinstsetting.GetValue("AutoUpdate") != null)
                {
                    int AutoUpdate = int.Parse(appinstsetting.GetValue("AutoUpdate").ToString());
                    if (AutoUpdate == 1)
                    {
                        AutoUpdater.Start("https://raw.githubusercontent.com/Endeade/endeade.github.io/main/appinstaller/autoupdater.xml");
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }
                else
                {

                }

                if (appinstsetting.GetValue("DarkMode") != null)
                {
                    int DarkMode = int.Parse(appinstsetting.GetValue("DarkMode").ToString());

                    if (DarkMode == 1)
                    {
                        Runtimes.BackColor = Color.Black;
                        Runtimes.ForeColor = Color.White;
                        button25.ForeColor = Color.Black;
                        button26.ForeColor = Color.Black;
                        Browsers.BackColor = Color.Black;
                        Browsers.ForeColor = Color.White;
                        ChatApps.BackColor = Color.Black;
                        ChatApps.ForeColor = Color.White;
                        Gaming.BackColor = Color.Black;
                        Gaming.ForeColor = Color.White;
                        Utilities.BackColor = Color.Black;
                        Utilities.ForeColor = Color.White;
                        Settings.BackColor = Color.Black;
                        Settings.ForeColor = Color.White;
                        this.BackColor = Color.Black;
                        this.ForeColor = Color.White;
                        button1.ForeColor = Color.Black;
                        button2.ForeColor = Color.Black;
                        button3.ForeColor = Color.Black;
                        button4.ForeColor = Color.Black;
                        button5.ForeColor = Color.Black;
                        button6.ForeColor = Color.Black;
                        button7.ForeColor = Color.Black;
                        button8.ForeColor = Color.Black;
                        button9.ForeColor = Color.Black;
                        button10.ForeColor = Color.Black;
                        button11.ForeColor = Color.Black;
                        button12.ForeColor = Color.Black;
                        button13.ForeColor = Color.Black;
                        button14.ForeColor = Color.Black;
                        button17.ForeColor = Color.Black;
                        button18.ForeColor = Color.Black;
                        button19.ForeColor = Color.Black;
                        button20.ForeColor = Color.Black;
                        button21.ForeColor = Color.Black;
                        button22.ForeColor = Color.Black;
                        button23.ForeColor = Color.Black;
                        button24.ForeColor = Color.Black;
                        checkBox2.Checked = true;
                    }
                    else
                    {
                        checkBox2.Checked = false;
                    }
                }
                else
                {

                }


            }
        }



        private async void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            button2.Text = "Installing through winget";
            progressBar1.Value = 50;
            var process = Process.Start("cmd", "/c winget install --id Google.Chrome");
            process.WaitForExit();
            progressBar1.Value = 100;
            wait(1000);
            progressBar1.Visible = false;
            button1.Text = "Installed";
            wait(5000);
            button2.Text = "Update";
            button7.Visible = true;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Value = 50;
            button3.Text = "Installing through winget...";
            var process = Process.Start("cmd", "/c winget install --id Mozilla.Firefox");
            process.WaitForExit();
            progressBar1.Value = 100;
            wait(1000);
            progressBar1.Visible = false;
            button3.Text = "Installed";
            wait(5000);
            button3.Text = "Update";
            button8.Visible = true;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Value = 50;
            button4.Text = "Installing through winget...";
            var process = Process.Start("cmd", "/c winget install --id Vivaldi.Vivaldi");
            process.WaitForExit();
            progressBar1.Value = 100;
            wait(1000);
            progressBar1.Visible = false;
            button4.Text = "Installed";
            wait(5000);
            button4.Text = "Update";
            button9.Visible = true;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            button6.Text = "Installing through winget";
            progressBar1.Value = 50;
            var process = Process.Start("cmd", "/c winget install --id Discord.Discord");
            process.WaitForExit();
            progressBar1.Value = 100;
            wait(1000);
            progressBar1.Visible = false;
            button1.Text = "Installed";
            wait(5000);
            button6.Text = "Update";
            button10.Visible = true;
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
            button10.Text = "Uninstalling...";
            var process = Process.Start("cmd", "/c winget uninstall --id Discord.Discord");
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
            bool is64bitOS = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            if (is64bitOS)
            {
                string uninst = GetEnvironmentVariable("ProgramFiles(x86)") + "\\Steam\\uninstall.exe";
                button23.Text = "Uninstalling...";
                var process = Process.Start(uninst, "/S");
                process.WaitForExit();
                button23.Text = "Uninstalled";
                wait(5000);
                button23.Visible = false;
                button21.Text = "Install";
            } else
            {
                string uninst = GetEnvironmentVariable("ProgramFiles") + "\\Steam\\uninstall.exe";
                button23.Text = "Uninstalling...";
                var process = Process.Start(uninst, "/S");
                process.WaitForExit();
                button23.Text = "Uninstalled";
                wait(5000);
                button23.Visible = false;
                button21.Text = "Install";
            }
            
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
            button18.Text = "Downloading...";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += DownloadProgress;
            await client.DownloadFileTaskAsync(new Uri("https://github.com/PrismLauncher/PrismLauncher/releases/download/7.1/PrismLauncher-Windows-MSVC-Setup-7.1.exe"), downloadpath);
            button18.Text = "Installing...";
            var process = Process.Start(downloadpath, "/qn");
            process.WaitForExit();
            progressBar1.Visible = false;
            button18.Text = "Installed";
            wait(5000);
            button18.Text = "Update";
            button17.Visible = true;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string uninst = GetEnvironmentVariable("%localappdata%") + "\\Programs\\PrismLauncher\\uninstall.exe";
            button17.Text = "Uninstalling...";
            var process = Process.Start(uninst);
            process.WaitForExit();
            button17.Text = "Uninstalled";
            wait(5000);
            button17.Visible = false;
            button18.Text = "Install";

        }

        public void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                Browsers.BackColor = Color.Black;
                Browsers.ForeColor = Color.White;
                ChatApps.BackColor = Color.Black;
                ChatApps.ForeColor = Color.White;
                Gaming.BackColor = Color.Black;
                Gaming.ForeColor = Color.White;
                Utilities.BackColor = Color.Black;
                Utilities.ForeColor = Color.White;
                Settings.BackColor = Color.Black;
                Settings.ForeColor = Color.White;
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                button1.ForeColor = Color.Black;
                button2.ForeColor = Color.Black;
                button3.ForeColor = Color.Black;
                button4.ForeColor = Color.Black;
                button5.ForeColor = Color.Black;
                button6.ForeColor = Color.Black;
                button7.ForeColor = Color.Black;
                button8.ForeColor = Color.Black;
                button9.ForeColor = Color.Black;
                button10.ForeColor = Color.Black;
                button11.ForeColor = Color.Black;
                button12.ForeColor = Color.Black;
                button13.ForeColor = Color.Black;
                button14.ForeColor = Color.Black;
                button17.ForeColor = Color.Black;
                button18.ForeColor = Color.Black;
                button19.ForeColor = Color.Black;
                button20.ForeColor = Color.Black;
                button21.ForeColor = Color.Black;
                button22.ForeColor = Color.Black;
                button23.ForeColor = Color.Black;
                button24.ForeColor = Color.Black;
            }
            else
            {
                Browsers.BackColor = Color.White;
                Browsers.ForeColor = Color.Black;
                ChatApps.BackColor = Color.White;
                ChatApps.ForeColor = Color.Black;
                Gaming.BackColor = Color.White;
                Gaming.ForeColor = Color.Black;
                Utilities.BackColor = Color.White;
                Utilities.ForeColor = Color.Black;
                Settings.BackColor = Color.White;
                Settings.ForeColor = Color.Black;
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                button1.ForeColor = Color.Black;
                button2.ForeColor = Color.Black;
                button3.ForeColor = Color.Black;
                button4.ForeColor = Color.Black;
                button5.ForeColor = Color.Black;
                button6.ForeColor = Color.Black;
                button7.ForeColor = Color.Black;
                button8.ForeColor = Color.Black;
                button9.ForeColor = Color.Black;
                button10.ForeColor = Color.Black;
                button11.ForeColor = Color.Black;
                button12.ForeColor = Color.Black;
                button13.ForeColor = Color.Black;
                button14.ForeColor = Color.Black;
                button17.ForeColor = Color.Black;
                button18.ForeColor = Color.Black;
                button19.ForeColor = Color.Black;
                button20.ForeColor = Color.Black;
                button21.ForeColor = Color.Black;
                button22.ForeColor = Color.Black;
                button23.ForeColor = Color.Black;
                button24.ForeColor = Color.Black;
            }
        }

        async private void button26_Click(object sender, EventArgs e)
        {
            bool is64bitOS = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            if (is64bitOS)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string downloadpath = appinstallerfiles + "\\vcredist.exe";
                progressBar1.Visible = true;
                button26.Text = "Downloading...";
                WebClient client = new WebClient();
                client.DownloadProgressChanged += DownloadProgress;
                await client.DownloadFileTaskAsync(new Uri("https://aka.ms/vs/17/release/vc_redist.x64.exe"), downloadpath);
                button26.Text = "Installing...";
                var process = Process.Start(downloadpath, "/qn");
                process.WaitForExit();
                progressBar1.Visible = false;
                button18.Text = "Installed";
                wait(5000);
                button26.Text = "Update";
                button25.Text = "Uninstall unsupported";
            } else
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string downloadpath = appinstallerfiles + "\\vcredist.exe";
                progressBar1.Visible = true;
                button26.Text = "Downloading...";
                WebClient client = new WebClient();
                client.DownloadProgressChanged += DownloadProgress;
                await client.DownloadFileTaskAsync(new Uri("https://aka.ms/vs/17/release/vc_redist.x86.exe"), downloadpath);
                button26.Text = "Installing...";
                var process = Process.Start(downloadpath, "/qn");
                process.WaitForExit();
                progressBar1.Visible = false;
                button18.Text = "Installed";
                wait(5000);
                button26.Text = "Update";
                button25.Text = "Uninstall unsupported";
            }

        }
    }
}
