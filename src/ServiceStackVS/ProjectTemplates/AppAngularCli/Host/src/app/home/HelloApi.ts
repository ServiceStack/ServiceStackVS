import { Component, Input, OnInit } from '@angular/core';
import { JsonServiceClient } from '@servicestack/client';
import { Hello } from '../shared/dtos';

@Component({
    selector: 'hello-api',
    template: `
    <div class="form-group">
        <ng-input placeholder="Your name" autocomplete="off"
            [(ngModel)]="name"
            (ngModelChange)="nameChanged($event)"></ng-input>

        <h3 class="result">{{ result }}</h3>
    </div>
    `,
    styleUrls: ['HelloApi.scss'],
})
export class HelloApiComponent implements OnInit {

    @Input() name: string;
    result: string;

    constructor(private client: JsonServiceClient) {}

    ngOnInit() {
        this.nameChanged(this.name);
    }

    async nameChanged(name: string) {
        if (name) {
            const r = await this.client.get(new Hello({ name }));
            this.result = r.result;
        } else {
            this.result = '';
        }
    }
}
