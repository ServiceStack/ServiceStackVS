import { Component } from '@angular/core';

@Component({
    template: `
        <div id="view1">
            <h3>{{ message }}</h3>
        </div>
    `,
    styleUrls: ['view1.scss']
})
export class View1Component {
    message = 'This is View 1';

    constructor() {
    }
}
