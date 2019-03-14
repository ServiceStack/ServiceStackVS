document.documentElement.className += ' web';
window.nativeHost = {
    quit: function () {
        window.close();
    },
    showAbout: function () {
        alert("$safeprojectname$ - ServiceStack + React");
    },
    ready: function () { },
    toggleFormBorder: function () { }
};
