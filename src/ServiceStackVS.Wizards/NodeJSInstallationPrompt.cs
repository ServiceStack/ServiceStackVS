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
    public partial class NodeJSInstallationPrompt : Form
    {
        public bool NodeFoundOnPath;

        public NodeJSInstallationPrompt()
        {
            InitializeComponent();
        }

        private void NodeJSInstallationPrompt_Enter(object sender, EventArgs e)
        {
            NodeFoundOnPath = CheckForInstallation();
            if (NodeFoundOnPath)
            {
                this.Close(); 
            }
        }

        private bool CheckForInstallation()
        {
            bool nodeFound = false;
            string nodeJsPath = "";
            if (TryResolveProgramFilesPath(out nodeJsPath))
            {
                nodeFound = File.Exists(Path.Combine(nodeJsPath, "node.exe"));
            }
            else
            {
                //Unable to workout system path, bailout
                //TODO advise user to restart visual studio after installing nodejs
            }
            return nodeFound;
        }

        private bool TryResolveProgramFilesPath(out string path)
        {
            string systemPath = System.Environment.GetEnvironmentVariable("SystemRoot");
            bool x86Path;
            bool x64Path;
            path = "";
            if (systemPath != null)
            {
                string systemDrive = systemPath.Substring(0, systemPath.IndexOf("\\", System.StringComparison.Ordinal) + 1);
                x86Path = Directory.Exists(Path.Combine(systemDrive, "Program Files (x86)\\nodejs"));
                x64Path = Directory.Exists(Path.Combine(systemDrive, "Program Files\\nodejs"));
                if (x86Path)
                {
                    path = Path.Combine(systemDrive, "Program Files (x86)\\nodejs");
                }

                if (x64Path)
                {
                    path = Path.Combine(systemDrive, "Program Files\\nodejs");
                }
            }

            if (!string.IsNullOrEmpty(path))
            {
                Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + path + ";" + Path.Combine(path, "node_modules\\npm\\bin"));
                return true;
            }
            return false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("http://nodejs.org/");

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NodeFoundOnPath = CheckForInstallation();
            if (NodeFoundOnPath)
            {
                this.Close();
            }
        }
    }
}
