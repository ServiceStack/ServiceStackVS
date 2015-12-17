using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using ServiceStackVS.Common;

namespace ServiceStackVS.Settings
{
    public partial class ServiceStackGeneralSettingsPane : UserControl
    {
        [Import]
        public SVsServiceProvider ServiceProvider { get; set; }

        public WritableSettingsStore SettingsStore { get; set; }

        public ServiceStackGeneralSettingsPane()
        {
            InitializeComponent();
        }

        internal ServiceStackOptionsPage OptionsPage;

        public void Initialize()
        {
            SettingsStore = ServiceProvider.GetWritableSettingsStore();
            OptionsPage.OptOutOfStats = SettingsStore.GetBoolean("ServiceStackVS", "OptOutOfStats");
        }

        private void optOutStatsChkBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsStore.SetBoolean("ServiceStackVS","OptOutOfStats", optOutStatsChkBox.Checked);
            OptionsPage.OptOutOfStats = optOutStatsChkBox.Checked;
        }
    }
}
