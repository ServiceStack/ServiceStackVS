/// <reference path='../typings/browser.d.ts'/>

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