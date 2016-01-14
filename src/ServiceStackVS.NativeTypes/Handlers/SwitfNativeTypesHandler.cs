namespace ServiceStackVS.NativeTypes.Handlers
{
    public class SwitfNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.Swift; } }
        public override string CodeFileExtension { get { return ".dtos.swift"; } }
        public override string RelativeTypesUrl { get { return "types/swift"; } }
    }
}
