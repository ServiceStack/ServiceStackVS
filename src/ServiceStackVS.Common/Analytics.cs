using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace ServiceStackVS.Common
{
    public static class Analytics
    {
        public const string VERSION = "1.0.47";
        private const string ServiceStackStatsUrl = "https://servicestack.net/stats/ssvs{0}/record?name={1}&source=ssvs&version=" + VERSION;
        private const string ServiceStackStatsAddRefUrl = "https://servicestack.net/stats/addref/record?name={0}&source=ssvs&version=" + VERSION;
        private const string ServiceStackStatsUpdateRefUrl = "https://servicestack.net/stats/updateref/record?name={0}&source=ssvs&version=" + VERSION;

        static readonly Dictionary<int, string> VersionAlias = new Dictionary<int, string>
        {
            { 11, "2012" },
            { 12, "2013" },
            { 14, "2015" },
            { 15, "2017" },
            { 16, "2019" },
        };

        static string GetVersion(int vsVersion) => VersionAlias.TryGetValue(vsVersion, out var version) ? version : "";

        public static void SubmitAnonymousTemplateUsage(int vsVersion, string templatePath)
        {
            if (Environment.GetEnvironmentVariable("SERVICESTACK_TELEMETRY_OPTOUT") == "1") return;
            Task.Run(() =>
            {
                try
                {
                    var templateName = WizardHelpers.GetTemplateNameFromPath(templatePath);
                    ServiceStackStatsUrl.Fmt(GetVersion(vsVersion), templateName).GetStringFromUrl();
                }
                catch
                {
                    //do nothing
                }
            });
        }

        public static void SubmitAnonymousAddReferenceUsage(string languageName)
        {
            if (Environment.GetEnvironmentVariable("SERVICESTACK_TELEMETRY_OPTOUT") == "1") return;
            if (languageName == null) return;
            Task.Run(() =>
            {
                try
                {
                    ServiceStackStatsAddRefUrl.Fmt(languageName.ToLower()).GetStringFromUrl();
                }
                catch
                {
                    //do nothing
                }
            });
        }

        public static void SubmitAnonymousUpdateReferenceUsage(string languageName)
        {
            if (Environment.GetEnvironmentVariable("SERVICESTACK_TELEMETRY_OPTOUT") == "1") return;
            if (languageName == null) return;
            Task.Run(() =>
            {
                try
                {
                    ServiceStackStatsUpdateRefUrl.Fmt(languageName.ToLower()).GetStringFromUrl();
                }
                catch
                {
                    //do nothing
                }
            });
        }
    }
}
