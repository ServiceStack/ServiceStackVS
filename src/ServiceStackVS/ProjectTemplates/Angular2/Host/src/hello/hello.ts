import { Component, Input, OnChanges } from '@angular/core';
import { JsonServiceClient } from 'servicestack-client';
import { Hello } from './../dtos';

@Component({
    selector: 'hello',
    templateUrl: './src/hello/hello.html'
})
export class HelloComponent{
    result: string;
    @Input() name: string;
    client: JsonServiceClient;

    constructor() {
        this.client = new JsonServiceClient('/');
    }

    ngOnInit() {
        this.nameChanged(this.name);
    }

    nameChanged(newValue) {
        if (newValue != null && newValue.length > 0) {
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