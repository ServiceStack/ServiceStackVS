using System.Xml.Linq;
using NUnit.Framework;
using ServiceStackVS.NuGetInstallerWizard;

namespace ServiceStackVS.Tests
{
    [TestFixture]
    public class NuGetWizardTests
    {
        [Test]
        public void ExtractNuGetPackagesFromWizardData()
        {
            string wizardData = "<WizardData><packages><package id=\"ServiceStack\" version=\"latest\" /></packages></WizardData>";
            XElement element = XElement.Parse(wizardData);
            var extractedNuGetPackage = element.ExtractNuGetPackages();
            Assert.AreEqual(extractedNuGetPackage.Count,1);
        }
    }
}
