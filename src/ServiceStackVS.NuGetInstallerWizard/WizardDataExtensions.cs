using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ServiceStack;

namespace ServiceStackVS.NuGetInstallerWizard
{
    public static class WizardDataExtensions
    {
        public static List<NuGetWizardDataPackage> ExtractNuGetPackages(this XElement element)
        {
            return element
                .Descendants()
                .Where(x => x.Name.LocalName.EqualsIgnoreCase("package"))
                .Select(
                    x =>
                        new NuGetWizardDataPackage
                        {
                            Id = x.Attribute("id").Value,
                            Version = x.Attribute("version").Value
                        }).ToList();
        }

        public static bool HasRootPackage(this XElement wizardData)
        {
            return wizardData
                .Descendants()
                .FirstOrDefault(
                    x =>
                        x.Name.LocalName.EqualsIgnoreCase("package") && x.HasAttributes &&
                        x.Attribute("rootPackage") != null) != null;
        }

        public static NuGetWizardDataPackage GetRootPackage(this XElement wizardData)
        {
            return wizardData
                .Descendants()
                .Where(
                    x =>
                        x.Name.LocalName.EqualsIgnoreCase("package") && x.HasAttributes &&
                        x.Attribute("rootPackage") != null)
                .Select(x => new NuGetWizardDataPackage
                {
                    Id = x.Attribute("id").Value,
                    Version = x.Attribute("version").Value
                }).FirstOrDefault();
        }
    }
}
