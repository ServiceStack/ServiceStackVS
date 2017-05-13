import "./hello.css";

import { bindable, autoinject } from "aurelia-framework";
import { client } from "../../shared";
import { Hello } from "../../dtos";

@autoinject()
export class HelloCustomElement {
    result: string;
    @bindable name: string;

    async nameChanged(name:string) {
        if (name) {
            var req = new Hello();
            req.name = name;
            const r = await client.get(req);
            this.result = r.result;            
        } else {
            this.result = '';
        }
    }
}