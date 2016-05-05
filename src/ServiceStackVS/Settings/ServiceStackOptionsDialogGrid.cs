using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace ServiceStackVS.Settings
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class ServiceStackOptionsDialogGrid : DialogPage
    {
        [Category("General")]
        [DisplayName(@"Opt out of anonymous usage statistics")]
        [Description("Anonymous usage statistics are collected regarding use of templates to help ServiceStack focus on what provides you with the most value.")]
        public bool OptOutStats { get; set; }
    }
}
