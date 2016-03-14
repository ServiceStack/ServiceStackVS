interface Window {
    NativeHost: $safeprojectname$.Platform.INativeHost;
}
declare module $safeprojectname$.Platform {
    export interface INativeHost {
        showAbout();
        toggleFormBorder();
        quit();
    }
}
