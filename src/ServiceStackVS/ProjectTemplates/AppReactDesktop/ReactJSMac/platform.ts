class MacNativeHost {
    showAbout() {
        alert("ReactDesktopTest - ServiceStack + ReactJS");
    }

    toggleFormBorder() {}

    quit() {
        window.close();
    }
	
    ready() {}
}

window['nativeHost'] = window['nativeHost'] || new MacNativeHost();