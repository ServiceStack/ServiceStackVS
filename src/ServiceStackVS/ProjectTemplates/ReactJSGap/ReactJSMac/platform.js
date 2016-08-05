/* mac */
document.documentElement.className += ' mac';
window.nativeHost = {
    quit: function () {
        post('/nativehost/quit');
    },
    showAbout: function () {
    	post('/nativehost/showAbout');
    },
    ready: function () {
        //
    },
    platform: 'mac'
};

function post(url, data, callback) {
    var r = new XMLHttpRequest();
    r.open("POST", url, true);
    r.onreadystatechange = function () {
        if (r.readyState != 4 || r.status != 200)
            return;
        callback(r.responseText);
    };
    r.send(data);
}