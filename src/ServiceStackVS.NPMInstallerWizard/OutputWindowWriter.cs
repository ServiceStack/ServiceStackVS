using System;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ServiceStackVS.NPMInstallerWizard
{
    public class OutputWindowWriter
    {
        private readonly IVsOutputWindowPane outputWindowPane;

        public OutputWindowWriter(string outputWindowPaneGuid, string outputWindowPaneName)
        {
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
            var outputWindow = dte.Windows.Item("{34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3}");
            if (outputWindow != null)
            {
                outputWindow.Visible = true;
            }
        }

        public void Write(string format, params object[] parameters)
        {
            if (outputWindowPane == null || format == null) return;

            outputWindowPane.OutputString(String.Format(format, parameters));
        }

        public void WriteLine(string format, params object[] parameters)
        {
            Write(format + Environment.NewLine, parameters);
        }

        public void Clear()
        {
            outputWindowPane.Clear();
        }
    }
}
