import { Component } from '@angular/core';
import { HelloComponent } from './../hello/hello';

@Component({
    moduleId: module.id,
    selector: 'view-two',
    template: `
    <div>
        <h2>{{message}}</h2>
        <hello [name]="'from view 2!'"></hello>
    </div>`,
    directives: [HelloComponent]
})
export class View2Component {
    message: string;

    constructor() {
        this.message = "View 2";
    }
}