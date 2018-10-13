namespace ServiceStackVS.NativeTypes.Handlers
{
    public class KotlinNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage => NativeTypesLanguage.Kotlin;
        public override string CodeFileExtension => ".kt";
        public override string RelativeTypesUrl => "types/kotlin";
    }
}
