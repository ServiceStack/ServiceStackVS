/* winforms */
document.documentElement.className += ' winforms';

$(document).ready(function () {
    $("body").append('<div class="alert alert-success alert-hide" id="alertUpdate" style="position:absolute;width:100%;top:0;" role="alert">Update available! ' +
    '<a class="close" id="closeUpdate">×</a><button class="btn btn-primary" type="button" onclick="updateNow();">Update Now!</button>' +
    '</div>');
    $("#closeUpdate").on("click", function (e) {
        e.stopPropagation();
        e.preventDefault();
        $(this).closest(".alert").slideUp(400);
    });
    window.nativeHost.ready();

    $(document).on('keydown', function(e) {
        if (e.altKey && e.which == Keys.LEFT) {
            history.back();
        } else if (e.altKey && e.which == Keys.RIGHT) {
            history.forward();
        }
    });

    window.updateAvailable = function () {
        setTimeout(function () {
            $("#alertUpdate").slideDown(800);
        }, 1500);
    };
    window.updateNow = function () {
        setTimeout(function () {
            $("#alertUpdate")[0].innerHTML = 'Applying update.. Please wait, application will restart..';
        }, 500);
        window.nativeHost.performUpdate();
    };
});