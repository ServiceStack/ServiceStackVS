using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EnvDTE;
using ServiceStack;
using ServiceStack.Text;
using ServiceStackVS.Common;
using ServiceStackVS.FileHandlers;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NPMInstallerWizard;

namespace ServiceStackVS
{
    public static class VsixDocumentExtensions
    {
        private static bool _npmInstallRunning;
        private static bool _bowerInstallRunning;

        private static readonly object NpmStartingLock = new object();
        private static readonly object NpmRunningLock = new object();
        private static readonly object BowerStartingLock = new object();
        private static readonly object BowerRunningLock = new object();

        public static List<INativeTypesHandler> GetTypeHandlerForSelectedFiles(this IList<SelectedItem> files)
        {
            if (files == null || files.Count == 0)
            {
                return new List<INativeTypesHandler>();
            }
            return files
                .Select(
                    file => NativeTypeHandlers.All.FirstOrDefault(handler => handler.IsHandledFileType(file.Name.ToLowerInvariant()))
                )
                .Where(handler => handler != null).ToList();
        }

        public static void HandleDocumentSaved(this Document document, OutputWindowWriter windowWriter)
        {
            DocumentSavedHandlers.HandleDocumentSaved(document, windowWriter);
        }

        public static bool IsNpmUpdateDisable(this Document document)
        {
            string path = document.GetProjectPath();
            string settingsFilePath = Path.Combine(path, "servicestack.vsconfig");
            bool npmInstallDisabled = false;
            if (settingsFilePath.FileExists())
            {
                var settings = File.ReadAllText(settingsFilePath).ParseKeyValueText(" ");
                string disableNpmInstallOnSave;
                if (settings.TryGetValue("DisableNpmInstallOnSave", out disableNpmInstallOnSave))
                {
                    npmInstallDisabled = disableNpmInstallOnSave.EqualsIgnoreCase("true");
                }
            }
            return npmInstallDisabled;
        }

        public static bool IsBowerUpdateDisabled(this Document document)
        {
            string path = document.GetProjectPath();
            string settingsFilePath = Path.Combine(path, "servicestack.vsconfig");
            bool bowerInstallDisabled = false;
            if (settingsFilePath.FileExists())
            {
                var settings = File.ReadAllText(settingsFilePath).ParseKeyValueText(" ");
                string disableBowerInstallOnSave;
                if (settings.TryGetValue("DisableBowerInstallOnSave", out disableBowerInstallOnSave))
                {
                    bowerInstallDisabled = disableBowerInstallOnSave.EqualsIgnoreCase("true");
                }
            }
            return bowerInstallDisabled;
        }

        public static bool IsUpdateReferenceOnSaveDisabled(this Document document)
        {
            string path = document.GetProjectPath();
            string settingsFilePath = Path.Combine(path, "servicestack.vsconfig");
            bool updateReferenceOnSaveDisabled = false;
            if (settingsFilePath.FileExists())
            {
                var settings = File.ReadAllText(settingsFilePath).ParseKeyValueText(" ");
                string disableUpdateOnSave;
                if (settings.TryGetValue("DisableUpdateReferenceOnSave", out disableUpdateOnSave))
                {
                    updateReferenceOnSaveDisabled = disableUpdateOnSave.EqualsIgnoreCase("true");
                }
            }
            return updateReferenceOnSaveDisabled;
        }

        public static string GetProjectPath(this Document document)
        {
            string projectFile = document.ProjectItem.ContainingProject.FullName;
            string path = projectFile.Substring(0, projectFile.LastIndexOf("\\", StringComparison.Ordinal) + 1);
            return path;
        }

        public static void TryRunNpmInstall(this Document document, OutputWindowWriter windowWriter)
        {
            lock (NpmStartingLock)
            {
                if (_npmInstallRunning)
                {
                    return;
                }
                windowWriter.Show();
                windowWriter.WriteLine("--- NPM install started ---");
                Task.Run(() =>
                {
                    try
                    {
                        NodePackageUtils.RunNpmInstall(document.Path,
                            (sender, args) =>
                            {
                                if (string.IsNullOrEmpty(args.Data))
                                {
                                    return;
                                }
                                string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                windowWriter.WriteLine(s);
                            },
                            (sender, args) =>
                            {
                                if (string.IsNullOrEmpty(args.Data))
                                {
                                    return;
                                }
                                string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                windowWriter.WriteLine(s);
                            });
                    }
                    catch (Exception e)
                    {
                        windowWriter.WriteLine(e.Message);
                    }

                    lock (NpmRunningLock)
                    {
                        _npmInstallRunning = false;
                    }
                    windowWriter.WriteLine("--- NPM install complete ---");
                });
                lock (NpmRunningLock)
                {
                    _npmInstallRunning = true;
                }
            }
        }

        public static void TryBowerInstall(this Document document, OutputWindowWriter windowWriter)
        {
            lock (BowerStartingLock)
            {
                if (_bowerInstallRunning)
                {
                    return;
                }
                windowWriter.Show();
                windowWriter.WriteLine("--- Bower install started ---");
                Task.Run(() =>
                {
                    try
                    {
                        NodePackageUtils.RunBowerInstall(document.Path,
                            (sender, args) =>
                            {
                                if (string.IsNullOrEmpty(args.Data))
                                {
                                    return;
                                }
                                string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                windowWriter.WriteLine(s);
                            },
                            (sender, args) =>
                            {
                                if (string.IsNullOrEmpty(args.Data))
                                {
                                    return;
                                }
                                string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                windowWriter.WriteLine(s);
                            });
                    }
                    catch (Exception e)
                    {
                        windowWriter.WriteLine(e.Message);
                    }

                    lock (BowerRunningLock)
                    {
                        _bowerInstallRunning = false;
                    }
                    windowWriter.WriteLine("--- Bower install complete ---");
                });
                lock (BowerRunningLock)
                {
                    _bowerInstallRunning = true;
                }
            }
        }
    }
}
