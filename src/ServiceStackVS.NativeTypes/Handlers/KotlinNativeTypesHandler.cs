namespace ServiceStackVS.NativeTypes.Handlers
{
    public class KotlinNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.Kotlin; } }
        public override string CodeFileExtension { get { return ".kt"; } }
        public override string RelativeTypesUrl { get { return "types/kotlin"; } }
    }
}
