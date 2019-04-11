//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using System.ComponentModel.Composition.Hosting;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;
//using System.Xml.Linq;
//using EnvDTE;
//using Microsoft.VisualStudio.ComponentModelHost;
//using Microsoft.VisualStudio.Shell;
//using Microsoft.VisualStudio.TemplateWizard;
//using NuGet;
//using NuGet.VisualStudio;
//using ServiceStack;
//using ServiceStackVS.Common;
//using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

//namespace ServiceStackVS.NuGetInstallerWizard
//{
//    //https://docs.microsoft.com/en-us/nuget/visual-studio-extensibility/nuget-api-in-visual-studio
//    public class NuGetPackageInstallerMultiProjectWizard : IWizard
//    {
//        //private const string NugetV2Url = "https://packages.nuget.org/api/v2";

//        private IComponentModel ComponentModel => (IComponentModel) Package.GetGlobalService(typeof(SComponentModel));

//        private IVsPackageInstallerServices nuGetPackageRepository;
//        private IVsPackageInstallerServices NuGetPackageRepository => nuGetPackageRepository ??
//            (nuGetPackageRepository = ComponentModel.GetService<IVsPackageInstallerServices>());

//        //private IPackageRepository cachedRepository;
//        //private IPackageRepository CachedRepository
//        //{
//        //    get
//        //    {
//        //        string userAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
//        //        string cachePath = Path.Combine(userAppData, "NuGet\\Cache");
//        //        return cachedRepository ??
//        //               (cachedRepository = PackageRepositoryFactory.Default.CreateRepository(cachePath));
//        //    }
//        //}

//        public int MajorVisualStudioVersion => int.Parse(_dte.Version.Substring(0, 2));

//        [Import]
//        public SVsServiceProvider ServiceProvider { get; set; }

//        public static NuGetWizardDataPackage RootNuGetPackage;
//        private DTE _dte;

//        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
//        {
//            _dte = (DTE)automationObject;
//            if (runKind == WizardRunKind.AsMultiProject)
//            {
//                using (var serviceProvider = new ServiceProvider((IServiceProvider)automationObject))
//                {
//                    var componentModel = (IComponentModel)serviceProvider.GetService(typeof(SComponentModel));
//                    using (var container = new CompositionContainer(componentModel.DefaultExportProvider))
//                    {
//                        container.ComposeParts(this);
//                    }
//                }

//                bool optOutOfStats = _dte.GetOptOutStatsSetting();
//                if (!optOutOfStats)
//                {
//                    Analytics.SubmitAnonymousTemplateUsage(MajorVisualStudioVersion, customParams[0] as string);
//                }

//                string wizardData = replacementsDictionary["$wizarddata$"];
//                XElement element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
//                if (element.HasRootPackage())
//                {
//                    RootNuGetPackage = element.GetRootPackage();
//                    RootNuGetPackage.Version = GetLatestVersionOfPackage(RootNuGetPackage.Id);
//                }
//            }
//        }

//        public void ProjectFinishedGenerating(Project project)
//        {
            
//        }

//        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
//        {
            
//        }

//        public bool ShouldAddProjectItem(string filePath)
//        {
//            return true;
//        }

//        public void BeforeOpeningFile(ProjectItem projectItem)
//        {
            
//        }

//        public void RunFinished()
//        {
            
//        }

//        private string GetLatestVersionOfPackage(string packageId)
//        {
//            return "5.5.0";
//            //try
//            //{
//            //    var package = NuGetPackageRepository.FindPackagesById(packageId).First(x => x.IsLatestVersion);
//            //    return package.Version.ToString();
//            //}
//            //catch (Exception)
//            //{
//            //    var package = CachedRepository.FindPackagesById(packageId)
//            //        .OrderByDescending(x => x.Version.ToString())
//            //        .FirstOrDefault();
//            //    if (package != null && package.Version != null)
//            //    {
//            //        return package.Version.ToString();
//            //    }
//            //    MessageBox.Show(
//            //        "Unable to connect to NuGet and no cached packages found for " + packageId,
//            //        "ServiceStackVS Error",
//            //        MessageBoxButtons.OK,
//            //        MessageBoxIcon.Error,
//            //        MessageBoxDefaultButton.Button1,
//            //        MessageBoxOptions.DefaultDesktopOnly
//            //    );
//            //    throw new WizardBackoutException("Unable to connect to NuGet and no cached packages found for " + packageId);
//            //}

//        }
//    }
//}
