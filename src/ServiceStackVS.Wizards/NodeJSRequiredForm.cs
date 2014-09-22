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
    public partial class NodeJsRequiredForm : Form
    {
        private bool gruntFoundOnPath;
        private bool gulpFoundOnPath;
        private bool bowerFoundOnPath;
        private string nodePath;

        public bool RequirementsMet = false;

        public NodeJsRequiredForm()
        {
            InitializeComponent();
        }

        public void UpdateProgress(object sender, PackageInstallEventArgs eventArgs)
        {
            this.Invoke((MethodInvoker)delegate
            {
                
                // runs on UI thread
                UpdateAll(eventArgs);
                RequirementsMet =
                    gruntFoundOnPath &&
                    gulpFoundOnPath &&
                    bowerFoundOnPath;
                if (RequirementsMet)
                {
                    this.Close();
                }
            });
        }

        private void UpdateAll(PackageInstallEventArgs eventArgs)
        {
            UpdatePaths(eventArgs);
            UpdateCheckList();
        }

        private void UpdatePaths(PackageInstallEventArgs eventArgs)
        {
            if (eventArgs.Package.Id == "grunt" && eventArgs.InstallationComplete) gruntFoundOnPath = eventArgs.InstallationComplete;
            if (eventArgs.Package.Id == "gulp" && eventArgs.InstallationComplete) gulpFoundOnPath = eventArgs.InstallationComplete;
            if (eventArgs.Package.Id == "bower" && eventArgs.InstallationComplete) bowerFoundOnPath = eventArgs.InstallationComplete;
        }

        private void UpdateCheckList()
        {
            installedItems.SetItemChecked(0, gruntFoundOnPath);
            installedItems.SetItemChecked(1, gulpFoundOnPath);
            installedItems.SetItemChecked(2, bowerFoundOnPath);
        }
    }
}
