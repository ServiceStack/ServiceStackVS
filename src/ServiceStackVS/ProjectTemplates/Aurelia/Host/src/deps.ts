/// <reference path='../typings/index.d.ts'/>

import {Aurelia} from 'aurelia-framework';
import {HttpClient} from "aurelia-http-client";
import {bootstrap} from 'aurelia-bootstrapper'
import {TemplatingBindingLanguage} from 'aurelia-templating-binding';
import {NumberRepeatStrategy} from 'aurelia-templating-resources';
import {ConsoleAppender} from 'aurelia-logging-console';
import {TemplatingRouteLoader} from 'aurelia-templating-router';

export class Deps {
    httpClient: HttpClient;
    aurelia: Aurelia;

    constructor() {
        var foo = new HttpClient();
        var a = new Aurelia();
        var b = bootstrap(null);
        var c = new TemplatingBindingLanguage();
        var d = new NumberRepeatStrategy();
        var e = new ConsoleAppender();
        var f = new TemplatingRouteLoader(null);
    }
}