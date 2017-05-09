import 'bootstrap/dist/css/bootstrap.css';
import "font-awesome/css/font-awesome.css";
import './app.css';

import {Aurelia} from 'aurelia-framework';

export function configure(aurelia: Aurelia) {
    aurelia.use
        .standardConfiguration()
        .feature('resources')
        .developmentLogging();

    aurelia.start().then(x => x.setRoot('app'));
}