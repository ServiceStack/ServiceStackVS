namespace ServiceStackVS.NativeTypes.Handlers
{
    public class JavaNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage => NativeTypesLanguage.Java;
        public override string CodeFileExtension => ".java";
        public override string RelativeTypesUrl => "types/java";
    }
}
