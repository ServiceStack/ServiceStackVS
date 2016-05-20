/// <reference path='../typings/browser.d.ts'/>
import {Router} from 'aurelia-router';

export class App {
    router: Router;

    configureRouter(config, router: Router) {
        config.title = 'Aurelia';
        config.map([
            { route: ['', 'home'], name: 'home', moduleId: './home', nav: true, title: 'Home' },
            { route: ['/view1', 'view1'], name: 'view1', moduleId: './view1', nav: true, title: 'View 1' },
            { route: ['/view2', 'view2'], name: 'view2', moduleId: './view2', nav: true, title: 'View 2' }
        ]);

        this.router = router;
    }
}