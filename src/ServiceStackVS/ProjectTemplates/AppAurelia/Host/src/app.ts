import { Router } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';

export class App {
    router: Router;

    configureRouter(config, router: Router) {
        config.title = 'Aurelia';
        config.options.pushState = true;
        config.map([
            { route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('views/home'), nav: true, title: 'Home', settings: { name: "Aurelia" } },
            { route: ['/view1', 'view1'], name: 'view1', moduleId: PLATFORM.moduleName('views/view1'), nav: true, title: 'View 1' },
            { route: ['/view2', 'view2'], name: 'view2', moduleId: PLATFORM.moduleName('views/view2'), nav: true, title: 'View 2' }
        ]);

        config.mapUnknownRoutes({ redirect: '' });

        this.router = router;
    }
}
