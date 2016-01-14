namespace ServiceStackVS.NativeTypes.Handlers
{
    public class JavaNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.Java; } }
        public override string CodeFileExtension { get { return ".java"; } }
        public override string RelativeTypesUrl { get { return "types/java"; } }
    }
}
