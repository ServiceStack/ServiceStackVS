/* mac */
document.documentElement.className += ' mac';
window.nativeHost = {
    quit: function () {
        $.post('/nativehost/quit');
    },
    showAbout: function () {
    	$.post('/nativehost/showAbout');
    },
    ready: function () {
        //
    },
    platform: 'mac'
};