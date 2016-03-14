// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='../$safeprojectname$/typings/browser.d.ts'/>
/// <reference path="../$safeprojectname$/platform.d.ts" />
document.documentElement.className += ' winforms';
$(document).ready(() => {
    window['nativeHost'].ready();

    $(document).on('keydown', e => {
        if (e.altKey && e.which === 37) {
            history.back();
        } else if (e.altKey && e.which === 39) {
            history.forward();
        }
    });
});