interface Window {
    nativeHost: $safeprojectname$.Platform.INativeHost;
}
declare module $safeprojectname$.Platform {
    export interface INativeHost {
        showAbout();
        toggleFormBorder();
        quit();
        ready();
    }
}
