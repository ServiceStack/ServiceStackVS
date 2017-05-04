import { bindable, autoinject } from "aurelia-framework";
import { JsonServiceClient } from "servicestack-client";
import { Hello } from "../../dtos";

let client = new JsonServiceClient('/');

@autoinject()
export class HelloCustomElement {
    result: string;
    @bindable name: string;

    nameChanged(newValue) {
        if (newValue.length > 0) {
            var req = new Hello();
            req.name = newValue;
            client.get(req).then(r => {
                this.result = r.result;
            });
        } else {
            this.result = '';
        }
    }
}