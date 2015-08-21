using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;

namespace $safeprojectname$
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            VerticalScroll.Visible = false;

            var chromiumBrowser = new ChromiumWebBrowser(Program.HostUrl)
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(chromiumBrowser);

            FormClosed += (sender, args) =>
            {
                Cef.Shutdown();
            };

            Load += (sender, args) =>
            {
                FormBorderStyle = FormBorderStyle.None;
                Left = Top = 0;
                Width = Screen.PrimaryScreen.WorkingArea.Width;
                Height = Screen.PrimaryScreen.WorkingArea.Height;
            };

            chromiumBrowser.RegisterJsObject("aboutDialog", new AboutDialogJsObject());
            chromiumBrowser.RegisterJsObject("winForm",new WinFormsApp(this));
        }
    }

    public class AboutDialogJsObject
    {
        public void Show()
        {
            MessageBox.Show(@"ServiceStack with CefSharp + ReactJS",@"$safeprojectname$", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class WinFormsApp
    {
        public FormMain Form { get; set; }

        public WinFormsApp(FormMain form)
        {
            Form = form;
        }

        public void Close()
        {
            Form.InvokeOnUiThreadIfRequired(() =>
            {
                Form.Close();  
            });
        }
    }
}
