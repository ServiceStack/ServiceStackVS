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
        private FormMain form;

        public WinFormsApp(FormMain form)
        {
            this.form = form;
        }

        public void Close()
        {
            form.InvokeOnUiThreadIfRequired(() =>
            {
                form.Close();  
            });
        }

        public void ToggleFormBorder()
        {
            form.InvokeOnUiThreadIfRequired(() => {
                form.FormBorderStyle = form.FormBorderStyle == FormBorderStyle.None
                    ? FormBorderStyle.Sizable
                    : FormBorderStyle.None;
            });
        }
    }
}
