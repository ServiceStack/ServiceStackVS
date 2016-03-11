// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='../$safeprojectname$/typings/browser.d.ts'/>
/// <reference path="../$safeprojectname$/platform.d.ts" />

import Platform = $safeprojectname$.Platform;

class ConsoleNativeHost implements Platform.INativeHost {
    showAbout() {
        alert('ReactDesktopApps222 - ServiceStack + ReactJS');
    }

    toggleFormBorder() {

    }

    quit() {
        $.post('/nativehost/quit').then(response => {
            window.close();
        });
    }
}

window['nativeHost'] = new ConsoleNativeHost();