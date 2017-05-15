import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    template: `
    <div mdl class="mdl-layout mdl-js-layout mdl-layout--fixed-header">

        <app-header></app-header>

        <main class="mdl-layout__content">

            <div class="mdl-layout__tab-panel is-active" id="overview">
                <router-outlet></router-outlet>
            </div>

            <h4 style="text-align: center">
                <img [src]="logoUrl" />
                <a [href]="url">Learn about this Angular4 VS.NET template</a>
            </h4>

            <app-footer></app-footer>

        </main>
    </div>
    `
})
export class AppComponent {
    url: string;
    logoUrl: string;

    constructor() {
        this.logoUrl = require("./assets/img/logo.png");
        this.url = 'https://servicestack.net/vs-templates/AngularApp';
    }
}
