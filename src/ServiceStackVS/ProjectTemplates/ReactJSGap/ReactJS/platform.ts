// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='platform.d.ts'/>

import INativeHost = $safeprojectname$.Platform.INativeHost;

export namespace NativeHost {

    export function getInstance(): INativeHost {
        return window['nativeHost'] as INativeHost;
    }
    export function showAbout() {
        getInstance().showAbout();
    }

    export function toggleFormBorder() {
        getInstance().toggleFormBorder();
    }

    export function quit() {
        getInstance().quit();
    }
}

class WebNativeHost implements INativeHost {
    showAbout() {
        alert("ReactDesktopApps3 - ServiceStack + ReactJS");
    }

    toggleFormBorder() {

    }

    quit() {
        window.close();
    }
}

window['nativeHost'] = new WebNativeHost();
