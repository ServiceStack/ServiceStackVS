/// <reference path='../typings/browser.d.ts'/>
import {Router} from 'aurelia-router';

export class App {
    router: Router;

    configureRouter(config, router: Router) {
        config.title = 'Aurelia';
        config.map([
            { route: ['', 'home'], name: 'home', moduleId: './hello', nav: true, title: 'Home' }
        ]);

        this.router = router;
    }
}