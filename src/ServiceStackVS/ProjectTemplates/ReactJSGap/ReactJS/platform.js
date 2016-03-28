document.documentElement.className += ' web';
var WebNativeHost = (function () {
    function WebNativeHost() {
    }
    WebNativeHost.prototype.showAbout = function () {
        alert("ReactDesktopTest - ServiceStack + ReactJS");
    };
    WebNativeHost.prototype.toggleFormBorder = function () { };
    WebNativeHost.prototype.quit = function () {
        window.close();
    };
    WebNativeHost.prototype.ready = function () { };
    return WebNativeHost;
})();
window.nativeHost = window.nativeHost || new WebNativeHost();
