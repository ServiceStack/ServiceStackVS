using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using NuGet.VisualStudio;

namespace ServiceStackVS.Common
{
    public static class NuGetCoreExtensions
    {
        public static string GetLatestVersionOfPackage(this IVsPackageRestorer packageRepository, string packageId)
        {
            var package = packageRepository.GetLatestVersionOfPackage(packageId);
            return package;
        }
    }

    public static class WizardHelpers
    {
        public static string GetTemplateNameFromPath(string vsTemplatePath)
        {
            string result = "";
            int lastBackSlashIndex = vsTemplatePath.LastIndexOf("\\", StringComparison.Ordinal);
            int dotVsTemplateIndex = vsTemplatePath.IndexOf(".vstemplate", StringComparison.Ordinal);
            result = vsTemplatePath.Substring(lastBackSlashIndex + 1, dotVsTemplateIndex - lastBackSlashIndex - 1);
            return result;
        }
    }

    public static class SettingsStorage
    {
        public const string CategoryName = "ServiceStack";
        public const string PageName = "General";
        public const string OptOutPropertyName = "OptOutStats";

        public const string PackageSettingsCategory = "ServiceStackSettings";
        public const string PackageReadyPropertyName = "PackageReady";
        
        public static bool GetOptOutStatsSetting(this DTE dte)
        {
            var props = dte?.get_Properties(CategoryName, PageName);
            return props?.Item(OptOutPropertyName)?.Value is bool b && b;
        }

        public static bool GetOptOutStatsSetting(this SVsServiceProvider vsServiceProvider)
        {
            var shellSettingsManager = new ShellSettingsManager(vsServiceProvider);
            return shellSettingsManager.GetReadOnlySettingsStore(SettingsScope.UserSettings).GetBoolean(CategoryName, OptOutPropertyName);
        }

        public static WritableSettingsStore GetWritableSettingsStore(this SVsServiceProvider vsServiceProvider)
        {
            var shellSettingsManager = new ShellSettingsManager(vsServiceProvider);
            return shellSettingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
        }

        public static void InitWritableSettings(this WritableSettingsStore settingsStore)
        {
            if (!settingsStore.CollectionExists(PackageSettingsCategory))
            {
                settingsStore.CreateCollection(PackageSettingsCategory);
            }

            if (!settingsStore.PropertyExists(PackageSettingsCategory, PackageReadyPropertyName))
            {
                settingsStore.SetBoolean(PackageSettingsCategory,PackageReadyPropertyName,false);
            }
        }

        public static void SetPackageReady(this WritableSettingsStore settingsStore, bool value)
        {
            settingsStore.InitWritableSettings();
            settingsStore.SetBoolean(PackageSettingsCategory,PackageReadyPropertyName, value);
        }

        public static bool GetPackageReady(this WritableSettingsStore settingsStore)
        {
            settingsStore.InitWritableSettings();
            return settingsStore.GetBoolean(PackageSettingsCategory, PackageReadyPropertyName);
        }
    }
}
