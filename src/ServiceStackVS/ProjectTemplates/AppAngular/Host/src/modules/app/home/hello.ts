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

    constructor(private cdref: ChangeDetectorRef) { }

    @Input() name: string;

    ngOnInit() {
        this.nameChanged(this.name);
    }

    async nameChanged(name: string) {
        if (name) {
            var req = new Hello();
            req.name = name;
            var r = await client.get(req);
            this.result = r.result;
        } else {
            this.result = '';
        }
        this.cdref.detectChanges();
    }
}