// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='../typings/browser.d.ts'/>
/// <reference path='../jspm_packages/npm/aurelia-framework@1.0.0-beta.1.2.2/aurelia-framework.d.ts'/>

import {Aurelia} from 'aurelia-framework';

export function configure(aurelia: Aurelia) {
    aurelia.use
        .standardConfiguration()
        .developmentLogging();

    aurelia.start().then(x => x.setRoot('src/app'));
}