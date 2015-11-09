using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet;
using ServiceStack;
using ServiceStackVS.Common;

namespace ServiceStackVS.ExternalTemplateWizard
{
    public class ExternalTemplateWizard : IWizard
    {
        private string templatesRootDir;
        private string externalTemplateDir;
        private string solutionDir;
        private string externalTemplateName;
        private string externalProjectPath;
        private string externalSolutionPath;
        private string projectName;
        private string safeProjectNameReplace;

        private string slnOutputName;
        private string projOutputName;
        private List<TemplatedFile> allTemplatedFiles = new List<TemplatedFile>();
        private Dictionary<string, string> localReplacementsDictionary;

        private const string nugetV2Url = "https://packages.nuget.org/api/v2";

        private IPackageRepository nuGetPackageRepository;
        private IPackageRepository NuGetPackageRepository
        {
            get
            {
                return nuGetPackageRepository ??
                       (nuGetPackageRepository =
                           PackageRepositoryFactory.Default.CreateRepository(nugetV2Url));
            }
        }

        private IPackageRepository _cachedRepository;
        private IPackageRepository CachedRepository
        {
            get
            {
                string userAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string cachePath = Path.Combine(userAppData, "NuGet\\Cache");
                return _cachedRepository ??
                       (_cachedRepository = PackageRepositoryFactory.Default.CreateRepository(cachePath));
            }
        }

        private const string ServiceStackVsOutputWindowPane = "5e5ab647-6a69-44a8-a2db-6a324b7b7e6d";
        private OutputWindowWriter _serviceStackOutputWindowWriter;
        private OutputWindowWriter OutputWindowWriter
        {
            get
            {
                return _serviceStackOutputWindowWriter ??
                    (_serviceStackOutputWindowWriter = new OutputWindowWriter(ServiceStackVsOutputWindowPane, "ServiceStackVS"));
            }
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            projectName = replacementsDictionary["$safeprojectname$"];
            localReplacementsDictionary = new Dictionary<string, string>(replacementsDictionary);

            string latestVersion = GetLatestVersionOfPackage("ServiceStack.Interfaces");
            localReplacementsDictionary.Add("$currentServiceStackVersion$",latestVersion);

            templatesRootDir = Path.GetDirectoryName(customParams[0] as string);
            localReplacementsDictionary.Add("$saferootprojectname$", projectName);
            if (templatesRootDir == null)
            {
                throw new WizardBackoutException("Failed to create project, 'customParams' does not contain extension template path.");
            }
            solutionDir = Path.GetDirectoryName(localReplacementsDictionary["$destinationdirectory$"]);
            string wizardData = localReplacementsDictionary["$wizarddata$"];
            XElement element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
            XElement externalTemplateRoot = null;
            if (element.Descendants().FirstOrDefault(x => x.Name.LocalName == "ExternalTemplate") != null)
            {
                externalTemplateRoot =
                    element.Descendants().First(x => x.Name.LocalName == "ExternalTemplate");
            }
            if (externalTemplateRoot == null)
            {
                return;
            }
            safeProjectNameReplace = externalTemplateRoot.GetAttributeValue("safeProjectNameReplace");
            externalTemplateName = externalTemplateRoot.GetExternalTemplateName();
            externalTemplateDir = Path.Combine(templatesRootDir, externalTemplateName);
            externalSolutionPath = Path.Combine(externalTemplateDir,
                externalTemplateRoot.GetExternalTemplateSolutionFileName());
            externalProjectPath = Path.Combine(externalTemplateDir,
                externalTemplateRoot.GetExternalTemplateProjectFileName());
            allTemplatedFiles = externalTemplateRoot.GetListOfTemplatedFiles();

            slnOutputName = externalTemplateRoot.GetAttributeValue("outputSolutionName");
            projOutputName = externalTemplateRoot.GetAttributeValue("outputProjectName");
        }

        public void ProjectFinishedGenerating(Project project)
        {
            if (externalSolutionPath == null)
            {
                return;
            }
            //Compute value for external template and replace 'safeprojectname' leaving 'saferootprojectname' as original value.
            if (safeProjectNameReplace != null)
            {
                localReplacementsDictionary["$safeprojectname$"] =
                   safeProjectNameReplace.ReplaceAllTokens(localReplacementsDictionary);
            }
            
            //Create solution
            string solutionContents = File.ReadAllText(externalSolutionPath);
            File.WriteAllText(Path.Combine(solutionDir, slnOutputName.ReplaceAllTokens(localReplacementsDictionary)), solutionContents.ReplaceAllTokens(localReplacementsDictionary));

            //Create proj
            string projectContents = File.ReadAllText(externalProjectPath);
            string projectContainerPath = Path.Combine(solutionDir, projectName);
            var projContainerInfo = Directory.CreateDirectory(projectContainerPath);
            string projOutputNameResult = projOutputName.ReplaceAllTokens(localReplacementsDictionary);
            string projectContentsPath = Path.Combine(projContainerInfo.FullName,
                projOutputNameResult.Substring(0, projOutputNameResult.Length - 7));
            var projPath = Directory.CreateDirectory(projectContentsPath);
            File.WriteAllText(Path.Combine(projPath.FullName, projOutputNameResult), projectContents.ReplaceAllTokens(localReplacementsDictionary));

            //Create files

            foreach (var templatedFile in allTemplatedFiles)
            {
                if (templatedFile.IsTemplatableFile())
                {
                    string templateContents = File.ReadAllText(Path.Combine(externalTemplateDir, templatedFile.Name));
                    string resultContents = templateContents.ReplaceAllTokens(localReplacementsDictionary);
                    string fileOutputPath = projPath.FullName;
                    if (templatedFile.Dest != null)
                    {
                        fileOutputPath =
                            Directory.CreateDirectory(Path.Combine(projPath.FullName, templatedFile.Dest)).FullName;
                    }
                    File.WriteAllText(Path.Combine(fileOutputPath, templatedFile.Name), resultContents);
                }
                else
                {
                    string fileOutputPath = projPath.FullName;
                    if (templatedFile.Dest != null)
                    {
                        fileOutputPath =
                            Directory.CreateDirectory(Path.Combine(projPath.FullName, templatedFile.Dest)).FullName;
                    }
                    File.Copy(Path.Combine(externalTemplateDir, templatedFile.Name), Path.Combine(fileOutputPath, templatedFile.Name));
                }
                
            }
        }

        private string GetLatestVersionOfPackage(string packageId)
        {
            string packageVersion;
            try
            {
                var package = NuGetPackageRepository.FindPackagesById(packageId).First(x => x.IsLatestVersion);
                packageVersion = package.Version.ToString();
            }
            catch (Exception)
            {
                OutputWindowWriter.WriteLine("Unable to get latest version number of '{0}' from NuGet. Falling back to cache.".Fmt(packageId));
                var cachedPackage = CachedRepository.FindPackagesById(packageId).OrderByDescending(x => x.Version).First(x => x.IsLatestVersion);
                packageVersion = cachedPackage.Version.ToString();
            }
            
            return packageVersion;
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

    public class TemplatedFile
    {
        public string Name { get; set; }
        public string Dest { get; set; }
    }

    public static class ExternalTemplateUtils
    {

        public static List<string> ExlcudedFileExtensions = new List<string>
        {
            "dll",
            "icns",
            "png",
            "jpg",
            "ico"
        };

        public static string GetExternalTemplateName(this XElement element)
        {
            return GetAttributeValue(element,"name");
        }

        public static bool IsTemplatableFile(this TemplatedFile templatedFile)
        {
            return !ExlcudedFileExtensions.Any(x => templatedFile.Name.EndsWith(x));
        }

        public static string GetExternalTemplateProjectFileName(this XElement element)
        {
            return GetAttributeValue(element, "projFile");
        }

        public static string GetExternalTemplateSolutionFileName(this XElement element)
        {
            return GetAttributeValue(element, "solutionFile");
        }

        public static List<TemplatedFile> GetListOfTemplatedFiles(this XElement element)
        {
            List<TemplatedFile> result = new List<TemplatedFile>();

            if (element.Descendants().Any(x => x.Name.LocalName == "Files") == false)
            {
                return result;
            }

            List<XElement> fileElements = element.Descendants().Where(x => x.Name.LocalName == "File").ToList();
            result = fileElements.Select(x => new TemplatedFile { Name = x.GetAttributeValue("name"), Dest = x.GetAttributeValue("dest") }).ToList();
            return result;
        }

        public static string GetAttributeValue(this XElement element, string attributeName)
        {
            string result = null;
            if (element.HasAttributes && element.Attributes().Any(x => x.Name.LocalName == attributeName))
            {
                result = element.Attributes().First(x => x.Name.LocalName == attributeName).Value;
            }
            return result;
        }

        public static string ReplaceAllTokens(this string contents, Dictionary<string, string> replacementsDictionary)
        {
            string result = contents;
            foreach (var pair in replacementsDictionary)
            {
                result = result.Replace(pair.Key, pair.Value);
            }
            return result;
        }
    }
}
