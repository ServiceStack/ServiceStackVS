using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace ServiceStackVS.Settings
{
    public class ServiceStackOptionsDialogGrid : DialogPage
    {
        [Category("General")]
        [DisplayName(@"Opt out of anonymous usage statistics")]
        [Description("Anonymous usage statistics are collected regarding use of templates to help ServiceStack focus on what provides you with the most value.")]
        public bool OptOutStats { get; set; }
    }
}
