// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
/// <reference path='../typings/browser.d.ts'/>
/// <reference path='../jspm_packages/npm/aurelia-framework@1.0.0-beta.1.2.2/aurelia-framework.d.ts'/>
import {bindable, ObserverLocator, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-http-client";

@autoinject()
export class HelloCustomElement {
    result: string;
    @bindable name: string;

    constructor(private httpClient: HttpClient, private observer: ObserverLocator) {
        this.httpClient.configure(config => {
            config.withHeader('Accept', 'application/json');
        });
        observer.getObserver(this, 'name')
            .subscribe(this.onChange);
    }

    onChange = () => {
        if (this.name.length > 0) {
            this.httpClient.get('/hello/' + this.name).then((response) => {
                this.result = response.content.Result;
            });
        } else {
            this.result = '';
        }
    }
}