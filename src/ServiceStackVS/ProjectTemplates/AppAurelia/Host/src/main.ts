import 'bootstrap/dist/css/bootstrap.css';
import "font-awesome/css/font-awesome.css";
import './app.css';

import { Aurelia } from 'aurelia-framework';
import { PLATFORM } from 'aurelia-pal';

export async function configure(aurelia: Aurelia) {
    aurelia.use
        .standardConfiguration()
        .developmentLogging();

    aurelia.use.plugin(PLATFORM.moduleName('resources'));

    await aurelia.start();
    await aurelia.setRoot(PLATFORM.moduleName('app'));
}
