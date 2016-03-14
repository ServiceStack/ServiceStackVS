// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='platform.d.ts'/>

import INativeHost = $safeprojectname$.Platform.INativeHost;

export namespace NativeHost {
    export function showAbout() {
        window.NativeHost.showAbout();
    }

    export function toggleFormBorder() {
        window.NativeHost.toggleFormBorder();
    }

    export function quit() {
        window.NativeHost.quit();
    }
}

class WebNativeHost implements INativeHost {
    showAbout() {
        alert("$safeprojectname$ - ServiceStack + ReactJS");
    }

    toggleFormBorder() {}

    quit() {
        window.close();
    }
}

window.NativeHost = window.NativeHost || new WebNativeHost();
