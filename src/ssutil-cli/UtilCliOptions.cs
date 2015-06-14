using System.Collections.Generic;
using Mono.Options;

namespace ssutil_cli
{
    public class UtilCliOptions
    {
        public static string DEFAULT { get { return "default"; } }
        public static string LANG { get { return "lang"; } }
        public static string FILE { get { return "file"; } }

        public Dictionary<string, string> Options { get; set; }

        public UtilCliOptions()
        {
            Options = new Dictionary<string, string>();
            defaultOptionSet = new OptionSet()
            {
                {
                    "<>","Url of the ServiceStack endpoint or file path of an existing reference",
                    val => { Options.Add(DEFAULT,val);}
                },
                {
                    "l|lang=","Specific language used when adding a ServiceStack reference",
                    val => { Options.Add(LANG,val); }
                },
                {
                    "f|file=","Name/path of the new ServiceStack reference",
                    val => { Options.Add(FILE,val);}
                }
            };
        }

        public OptionSet DefaultOptionSet
        {
            get { return defaultOptionSet; }
        }

        private readonly OptionSet defaultOptionSet;

        public static UtilCliOptions GetInstance()
        {
            return _instance ?? (_instance = new UtilCliOptions());
        }

        private static UtilCliOptions _instance;
    }
}
