using System;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ServiceStackVS.NuGetInstallerWizard
{
    public class OutputWindowWriter
    {
        private IVsOutputWindowPane _outputWindowPane;
        private readonly string outputWindowPaneGuid;

        public OutputWindowWriter(System.IServiceProvider serviceProvider, string outputWindowPaneGuid, string outputWindowPaneName)
        {
            var outputWindow = serviceProvider.GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            this.outputWindowPaneGuid = outputWindowPaneGuid;
            if (outputWindow == null) throw new Exception("Unable to create an output pane.");

            var paneGuid = new Guid(outputWindowPaneGuid);
            outputWindow.GetPane(ref paneGuid, out _outputWindowPane);
            if (_outputWindowPane == null)
            {
                outputWindow.CreatePane(ref paneGuid, outputWindowPaneName, 1, 0);
                outputWindow.GetPane(ref paneGuid, out _outputWindowPane);
            }
        }

        public void Show()
        {
            _outputWindowPane.Activate();
        }

        public OutputWindowWriter(string outputWindowPaneGuid, string outputWindowPaneName)
        {
            var outputWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            if (outputWindow == null) throw new Exception("Unable to create an output pane.");

            var paneGuid = new Guid(outputWindowPaneGuid);
            outputWindow.GetPane(ref paneGuid, out _outputWindowPane);
            if (_outputWindowPane == null)
            {
                outputWindow.CreatePane(ref paneGuid, outputWindowPaneName, 1, 0);
                outputWindow.GetPane(ref paneGuid, out _outputWindowPane);
            }
        }

        public void ShowOutputPane(DTE dte)
        {
            var outputWindow = dte.Windows.Item(outputWindowPaneGuid);
            if (outputWindow != null)
            {
                outputWindow.Visible = true;
            }
        }

        public void Write(string format, params object[] parameters)
        {
            if (_outputWindowPane == null || format == null) return;

            _outputWindowPane.OutputString(String.Format(format, parameters));
        }

        public void WriteLine(string format, params object[] parameters)
        {
            Write(format + Environment.NewLine, parameters);
        }

        public void Clear()
        {
            _outputWindowPane.Clear();
        }
    }
}
