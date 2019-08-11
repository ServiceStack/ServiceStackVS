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
                <i class="svg-logo svg-2x"></i>
                <a [href]="url">Learn about this Angular4 template</a>
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
        this.url = 'https://github.com/NetCoreTemplates/angular-lite-spa';
    }
}
