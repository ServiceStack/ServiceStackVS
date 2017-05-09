import "./hello.css";

import { bindable, autoinject } from "aurelia-framework";
import { client } from "../../shared";
import { Hello } from "../../dtos";

@autoinject()
export class HelloCustomElement {
    result: string;
    @bindable name: string;

    async nameChanged(newValue) {
        if (newValue.length > 0) {
            var req = new Hello();
            req.name = newValue;
            const r = await client.get(req);
            this.result = r.result;            
        } else {
            this.result = '';
        }
    }
}