using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceStackVS.Wizards
{
    public partial class GitBashInstallationPrompt : Form
    {
        public bool GitFoundOnPath { get; set; }

        public GitBashInstallationPrompt()
        {
            InitializeComponent();
        }

        private bool CheckForInstallation()
        {
            bool gitFound = false;
            string gitPath = "";
            if (TryResolveProgramFilesPath(out gitPath))
            {
                gitFound = File.Exists(Path.Combine(gitPath, "git.exe"));
            }
            return gitFound;
        }

        private bool TryResolveProgramFilesPath(out string path)
        {
            string systemPath = System.Environment.GetEnvironmentVariable("SystemRoot");
            string localPfPath = "Git\\bin";
            path = "";
            if (systemPath != null)
            {
                string systemDrive = systemPath.Substring(0, systemPath.IndexOf("\\", System.StringComparison.Ordinal) + 1);
                bool x86Path = Directory.Exists(Path.Combine(systemDrive, "Program Files (x86)\\" + localPfPath));
                bool x64Path = Directory.Exists(Path.Combine(systemDrive, "Program Files\\" + localPfPath));
                if (x86Path)
                {
                    path = Path.Combine(systemDrive, "Program Files (x86)\\" + localPfPath);
                }

                if (x64Path)
                {
                    path = Path.Combine(systemDrive, "Program Files\\" + localPfPath);
                }
            }

            if (!string.IsNullOrEmpty(path))
            {
                Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + path,EnvironmentVariableTarget.User);
                return true;
            }
            return false;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            GitFoundOnPath = CheckForInstallation();
            if (!GitFoundOnPath)
            {
                MessageBox.Show(@"Unable to detect Git installation. Please restart Visual Studio and try again.",
                    @"Unable to detect Git installation.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly,
                    false);
            }
            else
            {
                this.Close();
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            Process.Start("http://git-scm.com/downloads");
        } 
    }
}
