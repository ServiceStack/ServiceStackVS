/* mac */
document.documentElement.className += ' mac';
window.nativeHost = {
    quit: function () {
        $.post('/nativehost/quit');
    },
    showAbout: function () {
    	$.post('/nativehost/showAbout');
    },
    toggleFormBorder: function () {
        //
    },
    dockLeft: function () {
        //
    },
    dockRight: function () {
        //
    },
    ready: function () {
        //
    },
    platform: 'mac'
};
