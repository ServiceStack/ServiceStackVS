// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path="../$saferootprojectname$/platform.d.ts" />
/// <reference path='../$saferootprojectname$/typings/browser.d.ts'/>
import Platform = $saferootprojectname$.Platform;

class ConsoleNativeHost implements Platform.INativeHost {
    showAbout() {
        alert("$safeprojectname$ - ServiceStack + ReactJS");
    }

    toggleFormBorder() {}

    quit() {
        $.post('/nativehost/quit').always(_ => {

            window.close();
        });
    }
	
	ready() {}
}

window.nativeHost = new ConsoleNativeHost();