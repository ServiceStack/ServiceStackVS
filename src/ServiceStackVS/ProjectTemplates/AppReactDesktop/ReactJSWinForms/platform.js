/* winforms */
document.documentElement.className += ' winforms';

document.write('<div class="alert alert-success alert-hide" id="alertUpdate" role="alert">Update available! ' +
  '<span class="close" id="closeUpdate" onclick="closeUpdate()">×</span>' +
  '<button class="btn btn-primary" type="button" onclick="updateNow();">Update Now!</button>' +
'</div>');

window.updateAvailable = function () {
    setTimeout(function () {
        document.getElementById("alertUpdate").style.display = 'block';
    }, 500);
};
window.updateNow = function () {
    setTimeout(function () {
        document.getElementById("alertUpdate").innerHTML = 'Applying update.. Please wait, application will restart..';
    }, 500);
    window.nativeHost.performUpdate();
};
window.closeUpdate = function (e) {
    document.getElementById("alertUpdate").style.display = 'none';
};

window.onresize = function () {
    console.log('onresize');
    document.getElementById("content").style.height = '85%';
    setTimeout(function () {
        document.getElementById("content").style.height = '100%';
    }, 0);
};

window.nativeHost.ready();