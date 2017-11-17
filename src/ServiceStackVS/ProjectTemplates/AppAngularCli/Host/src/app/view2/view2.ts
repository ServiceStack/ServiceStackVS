import { Component } from '@angular/core';

@Component({
    template: `
        <div id="view2">
            <h3>{{ message }}</h3>
        </div>
    `
})
export class View2Component {
    message = 'This is View 2';
}
