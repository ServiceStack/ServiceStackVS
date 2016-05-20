import {bindable, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-http-client";

@autoinject()
export class HelloCustomElement {
    result: string;
    @bindable name: string;

    constructor(private httpClient: HttpClient) {
        this.httpClient.configure(config => {
            config.withHeader('Accept', 'application/json');
        });
    }

    nameChanged(newValue) {
        if (newValue.length > 0) {
            this.httpClient.get('/hello/' + newValue).then((response) => {
                this.result = response.content.Result;
            });
        } else {
            this.result = '';
        }
    }
}