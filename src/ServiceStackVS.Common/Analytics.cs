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
        private const string serviceStackStatsUrl = "https://servicestack.net/stats/ssvs{0}/record?Name={1}";
        private const string serviceStackStatsAddRefUrl = "https://servicestack.net/stats/addref/record?Name={1}";

        static readonly Dictionary<int, string> VersionAlias = new Dictionary<int, string>
                {
                    {11,"2012"},
                    {12,"2013"},
                    {14,""},
                };

        public static void SubmitAnonymousTemplateUsage(int vsVersion, string templatePath)
        {
            Task.Run(() =>
            {
                try
                {
                    var templateName = WizardHelpers.GetTemplateNameFromPath(templatePath);
                    serviceStackStatsUrl.Fmt(VersionAlias[vsVersion], templateName).GetStringFromUrl();
                }
                catch (Exception e)
                {
                    //do nothing
                }
            });
        }

        public static void SubmitAnonymousAddReferenceUsage(string languageName)
        {
            if (languageName == null)
            {
                return;
            }
            Task.Run(() =>
            {
                try
                {
                    serviceStackStatsAddRefUrl.Fmt(languageName.ToLower()).GetStringFromUrl();
                }
                catch (Exception e)
                {
                    //do nothing
                }
            });
        }
    }
}
