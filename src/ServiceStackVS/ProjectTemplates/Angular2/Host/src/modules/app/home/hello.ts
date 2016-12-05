import { Component, Input, OnChanges } from '@angular/core';
import { JsonServiceClient } from 'servicestack-client';
import { Hello } from '../../../dtos';

var client = new JsonServiceClient('/');

@Component({
    selector: 'hello',
    templateUrl: './src/modules/app/home/hello.html'
})
export class HelloComponent {
    result: string;

    @Input() routeParam: string;
    @Input() heading: string;

    ngOnInit() {
        this.nameChanged(this.routeParam);
    }

    nameChanged(newValue) {
        if (newValue != null && newValue.length > 0) {
            var req = new Hello();
            req.name = newValue;
            client.get(req).then(r => {
                this.result = r.result
            });
        } else {
            this.result = '';
        }
    }
}