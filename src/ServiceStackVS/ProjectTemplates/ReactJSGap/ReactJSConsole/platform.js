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
        alert('ReactChat - ServiceStack + ReactJS');
    },
    toggleFormBorder: function() {
        //
    },
    dockLeft: function() {
        //
    },
    dockRight: function() {
        //
    },
    ready: function() {
        //
    },
    platform: 'console'
};
