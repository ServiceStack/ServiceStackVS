using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace $safeprojectname$
{
    public partial class FormMain : Form
    {
        private readonly ChromiumWebBrowser chromiumBrowser;

        public FormMain()
        {
            InitializeComponent();

            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 273);

            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.VerticalScroll.Visible = false;
            this.FormBorderStyle = FormBorderStyle.None;

            WindowState = FormWindowState.Maximized;
            chromiumBrowser = new ChromiumWebBrowser(Program.HostUrl)
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(chromiumBrowser);

            this.FormClosed += (sender, args) =>
            {
                Cef.Shutdown();
            };

            chromiumBrowser.RegisterJsObject("aboutDialog", new AboutDialogJsObject(), camelCaseJavascriptNames: true);
        }
    }

    public class AboutDialogJsObject
    {
        public void Show()
        {
            MessageBox.Show("ServiceStack with CefSharp + ReactJS","$safeprojectname$", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
