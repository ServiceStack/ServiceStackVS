using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ServiceStackVS.Common
{
    public class OutputWindowWriter
    {
        private readonly IVsOutputWindowPane outputWindowPane;

        private const string outputWindowPaneGuid = "5e5ab647-6a69-44a8-a2db-6a324b7b7e6d";

        public OutputWindowWriter(string outputWindowPaneGuid, string outputWindowPaneName)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            var outputWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            if (outputWindow == null) throw new Exception("Unable to create an output pane.");
            var paneGuid = new Guid(outputWindowPaneGuid);
            outputWindow.GetPane(ref paneGuid, out outputWindowPane);
            if (outputWindowPane == null)
            {
                outputWindow.CreatePane(ref paneGuid, outputWindowPaneName, 1, 0);
                outputWindow.GetPane(ref paneGuid, out outputWindowPane);
            }
        }

        public void Show()
        {
            outputWindowPane.Activate();
        }

        public void ShowOutputPane(DTE dte)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            var outputWindow = dte?.Windows?.Item("{34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3}");
            if (outputWindow != null)
            {
                outputWindow.Visible = true;
            }
        }

        private static OutputWindowWriter serviceStackOutputWindowWriter;

        public static OutputWindowWriter WriterWindow
        {
            get
            {
                return serviceStackOutputWindowWriter ??
                    (serviceStackOutputWindowWriter = new OutputWindowWriter(outputWindowPaneGuid, "ServiceStackVS"));
            }
        }

        public void Write(string format)
        {
            if (outputWindowPane == null || format == null) return;

            outputWindowPane.OutputString(format);
        }

        public void WriteLine(string format)
        {
            Write(format + Environment.NewLine);
        }

        public void Clear()
        {
            outputWindowPane.Clear();
        }
    }
}
