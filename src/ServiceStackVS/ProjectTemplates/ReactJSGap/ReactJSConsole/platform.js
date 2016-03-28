/* console */
document.documentElement.className += ' console';
window.nativeHost = {
    quit: function() {
        $.post('/nativehost/quit')
            .then(function() {
                window.close();
            });
    },
    showAbout: function() {
        alert('ServiceStack SelfHost Console + React');
    },
    ready: function() {
        //
    },
    platform: 'console'
};