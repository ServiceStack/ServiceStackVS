using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet;
using ServiceStack;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace ServiceStackVS.NuGetInstallerWizard
{
    public class ExternalSolutionWizard : IWizard
    {
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (runKind == WizardRunKind.AsMultiProject)
            {
                string ssvsProjectTemplatesPath = null;
                string templateZipPath;
                using (var serviceProvider = new ServiceProvider((IServiceProvider)automationObject))
                {
                    var componentModel = (IComponentModel)serviceProvider.GetService(typeof(SComponentModel));
                    using (var container = new CompositionContainer(componentModel.DefaultExportProvider))
                    {
                        container.ComposeParts(this);
                    }
                    //Get version of VS running extension.
                    ssvsProjectTemplatesPath = GetSsvsProjectTemplatesPath(componentModel);
                }

                if (ssvsProjectTemplatesPath == null)
                {
                    throw new Exception("Unable to find SSVS installed..");
                }

                string wizardData = replacementsDictionary["$wizarddata$"];
                XElement element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
                var templateContainerElement = element.Descendants().FirstOrDefault(x => x.Name.LocalName.EqualsIgnoreCase("TemplateContainerName"));
                var foldersToInclude = element.Descendants().Where(x => x.Name.LocalName.EqualsIgnoreCase("FolderPath"));
                var filesToInclude = element.Descendants().Where(x => x.Name.LocalName.EqualsIgnoreCase("FilePath"));

                if (templateContainerElement == null)
                {
                    throw new Exception("Invalid template name. Can't find external solution to extract.");
                }
                string templateContainerName = templateContainerElement.Value;
                var languageName = element.Descendants().FirstOrDefault(x => x.Name.LocalName.EqualsIgnoreCase("LanguageName"));
                string langPath = Path.Combine(ssvsProjectTemplatesPath, languageName == null ? "CSharp" : languageName.Value);
                string languageZipPrefix = (languageName == null ? "CSharp" : languageName.Value).ToLower();
                templateZipPath = Path.Combine(langPath, "ServiceStack\\" + templateContainerName + "." + languageZipPrefix + ".zip");

                //Find extension path, folder name is generated

                //First get VS version?
                

                //Extract files from zip relative to this extension

                if (foldersToInclude.Any())
                {
                    //
                }


            }
        }

        private static string GetSsvsProjectTemplatesPath(IComponentModel componentModel)
        {
            string result = null;
            var vsVersion = componentModel.GetType().GetAssembly().GetName().Version.Major.ToString();
            //Append 0 after getting major version component. This is to match directory name.
            vsVersion += ".0";
            var extensionsFolder = "%LocalAppData%\\Microsoft\\VisualStudio\\{0}\\Extensions".Fmt(vsVersion);
            //Find generated folder containing SSVS extension by parsing mainfest files.
            var extensionFolderDirInfo = new DirectoryInfo(extensionsFolder);
            foreach (var directoryInfo in extensionFolderDirInfo.GetDirectories())
            {
                //Read manifest
                var manifestFileInfo = directoryInfo.GetFiles("*.vsixmanifest").FirstOrDefault();
                if (manifestFileInfo != null)
                {
                    var manifestContents = File.ReadAllText(manifestFileInfo.FullName);
                    var elements = XElement.Parse(manifestContents);
                    var identityElement =
                        elements.Descendants().FirstOrDefault(x => x.Name.LocalName.EqualsIgnoreCase("Identity"));
                    string id = identityElement == null ? null : identityElement.Attribute(XName.Get("Id")).Value;
                    //ServiceStackVS ID
                    if (id != null && id == "97413fa1-bad9-4cfb-a91c-c8d7b2c3c844")
                    {
                        result = Path.Combine(directoryInfo.FullName, "Output\\ProjectTemplates");
                    }
                }
            }
            return result;
        }

        public void ProjectFinishedGenerating(Project project)
        {

        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {

        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {

        }

        public void RunFinished()
        {

        }
    }
}
