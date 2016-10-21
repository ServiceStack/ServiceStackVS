import { Component } from '@angular/core';
import { HelloComponent } from './../hello/hello';

@Component({
    moduleId: module.id,
    selector: 'view-one',
    template: `
    <div>
        <h2>{{message}}</h2>
        <hello [name]="'from view 1!'"></hello>
    </div>`
})
export class View1Component {
    message: string;

    constructor() {
        this.message = "View 1";
    }
}