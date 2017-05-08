import { Component, Input, ChangeDetectorRef } from '@angular/core';
import { client } from '../../../shared/utils';
import { Hello } from '../../../dtos';

@Component({
    selector: 'hello',
    templateUrl: 'hello.html',
    styleUrls: ['hello.css']
})
export class HelloComponent {
    result: string;

    constructor(private cdref:ChangeDetectorRef){}

    @Input() routeParam: string;
    @Input() heading: string;

    ngOnInit() {
        this.nameChanged(this.routeParam);
    }

    async nameChanged(newValue) {
        if (newValue) {
            var req = new Hello();
            req.name = newValue;
            var r = await client.get(req);
            this.result = r.result;
            this.cdref.detectChanges();
        } else {
            this.result = '';
        }
    }
}