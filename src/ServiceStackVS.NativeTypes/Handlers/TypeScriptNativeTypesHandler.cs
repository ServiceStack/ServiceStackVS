using ServiceStack;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class TypeScriptNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.TypeScript; } }
        public override string CodeFileExtension { get { return ".dtos.d.ts"; } }
        public override string RelativeTypesUrl { get { return "types/typescript.d"; } }
        public override bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            return fileName.EndsWithIgnoreCase(CodeFileExtension) || fileName.EqualsIgnoreCase("dtos.d.ts");
        }
    }

    public class TypeScriptConcreteNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.TypeScriptConcrete; } }
        public override string CodeFileExtension { get { return ".dtos.ts"; } }
        public override string RelativeTypesUrl { get { return "types/typescript"; } }
        public override bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            return fileName.EndsWithIgnoreCase(CodeFileExtension) || fileName.EqualsIgnoreCase("dtos.ts");
        }
    }
}
