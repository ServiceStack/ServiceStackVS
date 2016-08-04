import {bindable, autoinject} from "aurelia-framework";
import {JsonServiceClient} from "servicestack-client";
import {Hello} from "../../dtos";

@autoinject()
export class HelloCustomElement {
    result: string;
    @bindable name: string;
    client: JsonServiceClient;

    constructor() {
        this.client = new JsonServiceClient('/');
    }

    nameChanged(newValue) {
        if (newValue.length > 0) {
            var req = new Hello();
            req.Name = newValue;
            this.client.get(req).then((helloResponse) => {
                this.result = helloResponse.Result
            });
        } else {
            this.result = '';
        }
    }
}