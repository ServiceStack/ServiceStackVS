/* console */
document.documentElement.className += ' console';
window.nativeHost = {
    quit: function () {
        var r = new XMLHttpRequest();
        r.open("POST", "/nativehost/quit", true);
        r.onreadystatechange = function () {
            if (r.readyState != 4 || r.status != 200)
                return;
            window.close();
        };
        r.send("");
    },
    showAbout: function () {
        alert('ServiceStack SelfHost Console + React');
    },
    ready: function () {
        //
    },
    platform: 'console'
};