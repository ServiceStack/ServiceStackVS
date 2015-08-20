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

            this.VerticalScroll.Visible = false;

            chromiumBrowser = new ChromiumWebBrowser(Program.HostUrl)
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(chromiumBrowser);

            this.FormClosed += (sender, args) =>
            {
                Cef.Shutdown();
            };

            this.Load += Form1_Load;

            chromiumBrowser.RegisterJsObject("aboutDialog", new AboutDialogJsObject(), camelCaseJavascriptNames: true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
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
