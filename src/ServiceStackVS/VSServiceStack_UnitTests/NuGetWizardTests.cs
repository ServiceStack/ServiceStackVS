using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NuGet;
using NuGet.VisualStudio;
using ServiceStackVS.NuGetInstallerWizard;
using ServiceStackVS.TestUtility.Mocks;

namespace ServiceStackVS_UnitTests
{
    [TestClass]
    public class NuGetWizardTests
    {
        [TestMethod]
        public void ExtractNuGetPackagesFromWizardData()
        {
            string wizardData = "<WizardData><packages><package id=\"ServiceStack\" version=\"latest\" /></packages></WizardData>";
            XElement element = XElement.Parse(wizardData);
            var extractedNuGetPackage = element.ExtractNuGetPackages();
            Assert.AreEqual(extractedNuGetPackage.Count,1);
        }
    }
}
