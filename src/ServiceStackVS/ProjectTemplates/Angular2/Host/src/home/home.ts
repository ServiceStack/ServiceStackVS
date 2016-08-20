import { Component } from '@angular/core';
import { HelloComponent } from './../hello/hello';

@Component({
    moduleId: module.id,
    selector: 'home',
    template: `
    <div>
        <h2>{{message}}</h2>
        <hello></hello>
    </div>`,
    directives: [HelloComponent]
})
export class HomeComponent {
    message: string;

    constructor() {
        this.message = "Home";
    }
}