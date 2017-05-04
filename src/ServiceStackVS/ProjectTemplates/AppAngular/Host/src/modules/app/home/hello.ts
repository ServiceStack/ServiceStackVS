import { Component, Input, OnChanges } from '@angular/core';
import { JsonServiceClient } from 'servicestack-client';
import { Hello } from '../../../dtos';

var client = new JsonServiceClient('/');

@Component({
    selector: 'hello',
    templateUrl: 'hello.html',
    styleUrls: ['home.css']
})
export class HelloComponent {
    result: string;

    @Input() routeParam: string;
    @Input() heading: string;

    ngOnInit() {
        this.nameChanged(this.routeParam);
    }

    async nameChanged(newValue) {
        if (newValue != null && newValue.length > 0) {
            var req = new Hello();
            req.name = newValue;
            var r = await client.get(req);
            this.result = r.result;
        } else {
            this.result = '';
        }
    }
}