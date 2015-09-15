$(document).ready(function () {
    window.nativeHost.ready();

    $(document).on('keydown', function(e) {
        if (e.altKey && e.which == 37) { //LEFT
            history.back();
        } else if (e.altKey && e.which == 39) { //RIGHT
            history.forward();
        }
    });
});