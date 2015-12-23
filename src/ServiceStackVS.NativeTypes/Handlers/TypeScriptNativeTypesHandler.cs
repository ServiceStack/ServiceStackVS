namespace ServiceStackVS.NativeTypes.Handlers
{
    public class TypeScriptNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.TypeScript; } }
        public override string CodeFileExtension { get { return ".dtos.d.ts"; } }
        public override string RelativeTypesUrl { get { return "types/typescript.d"; } }
    }

    public class TypeScriptConcreteNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.TypeScriptConcrete; } }
        public override string CodeFileExtension { get { return ".dtos.ts"; } }
        public override string RelativeTypesUrl { get { return "types/typescript"; } }
    }
}
