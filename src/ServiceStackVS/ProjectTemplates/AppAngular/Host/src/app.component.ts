import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    template: `
    <div class="mdl-layout mdl-js-layout mdl-layout--fixed-header">

        <app-header></app-header>

        <main class="mdl-layout__content">

            <div class="mdl-layout__tab-panel is-active" id="overview">
                <router-outlet></router-outlet>
            </div>

            <h4 style="text-align: center">
                <a [href]="url">Learn about this Angular2 VS.NET template</a>
            </h4>

            <app-footer></app-footer>

        </main>
    </div>
    `
})
export class AppComponent {
    url: string;

    constructor() {
        this.url = 'https://servicestack.net/vs-templates/Angular2App';
    }

    ngAfterViewInit() {
        if (typeof componentHandler !== "undefined") 
            componentHandler.upgradeDom();
    }
}
