var MacNativeHost = (function () {
    function MacNativeHost() {
    }
    MacNativeHost.prototype.showAbout = function () {
        alert("ReactDesktopTest - ServiceStack + ReactJS");
    };
    MacNativeHost.prototype.toggleFormBorder = function () { };
    MacNativeHost.prototype.quit = function () {
        window.close();
    };
    MacNativeHost.prototype.ready = function () { };
    return MacNativeHost;
}());
window['nativeHost'] = window['nativeHost'] || new MacNativeHost();
//# sourceMappingURL=platform.js.map