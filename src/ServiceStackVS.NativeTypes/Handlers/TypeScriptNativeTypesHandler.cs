using ServiceStack;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class TypeScriptNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage => NativeTypesLanguage.TypeScript;
        public override string CodeFileExtension => ".dtos.d.ts";
        public override string RelativeTypesUrl => "types/typescript.d";

        public override bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;
            return fileName.EndsWithIgnoreCase(CodeFileExtension) || fileName.EqualsIgnoreCase("dtos.d.ts");
        }
    }

    public class TypeScriptConcreteNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage => NativeTypesLanguage.TypeScriptConcrete;
        public override string CodeFileExtension => ".dtos.ts";
        public override string RelativeTypesUrl => "types/typescript";

        public override bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;
            return fileName.EndsWithIgnoreCase(CodeFileExtension) || fileName.EqualsIgnoreCase("dtos.ts");
        }
    }
}
