using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackVS.FileHandlers
{
    public interface INativeTypesHandler
    {
        Dictionary<string, string> ParseComments(string codeOutput);
        string GenerateUrl(string baseUrl, Dictionary<string, string> options);
        string GetUpdatedCode(string baseUrl, Dictionary<string, string> options);
        NativeTypesLanguage TypesLanguage { get; }
        string CodeFileExtension { get; }
        string RelativeTypesUrl { get; }
        bool IsHandledFileType(string fileName);
        List<string> ValidVsProjectTypeGuids { get; } 
    }

    public enum NativeTypesLanguage
    {
        CSharp,
        FSharp,
        VbNet,
        TypeScript
    }
}
