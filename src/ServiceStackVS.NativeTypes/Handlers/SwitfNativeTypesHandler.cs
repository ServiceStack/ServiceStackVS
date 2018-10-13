namespace ServiceStackVS.NativeTypes.Handlers
{
    public class SwitfNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage => NativeTypesLanguage.Swift;
        public override string CodeFileExtension => ".dtos.swift";
        public override string RelativeTypesUrl => "types/swift";
    }
}
