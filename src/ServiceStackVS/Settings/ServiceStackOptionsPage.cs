using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace ServiceStackVS.Settings
{
    [Guid(GuidList.guidOptionsDialog)]
    public class ServiceStackOptionsPage : DialogPage
    {
        public bool OptOutOfStats { get; set; }
        protected override IWin32Window Window
        {
            get
            {
                ServiceStackGeneralSettingsPane pane = new ServiceStackGeneralSettingsPane {OptionsPage = this};
                pane.Initialize();
                return pane;
            }
        }
    }
}
