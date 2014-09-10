using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceStackVS.Wizards
{
    public partial class NodeJsRequiredForm : Form
    {
        public NodeJsRequiredForm()
        {
            InitializeComponent();
        }

        private void nodeInstall_Click(object sender, EventArgs e)
        {
            Process.Start("http://nodejs.org/");
        }
    }
}
