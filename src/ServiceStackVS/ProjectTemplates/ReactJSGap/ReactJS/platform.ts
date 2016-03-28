interface Window {
    nativeHost: any;
}

class WebNativeHost {
    showAbout() {
        alert("$safeprojectname$ - ServiceStack + ReactJS");
    }

    toggleFormBorder() {}

    quit() {
        window.close();
    }
	
    ready() {}
}

window.nativeHost = window.nativeHost || new WebNativeHost();
