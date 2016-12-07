import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderComponent } from './shared/header';
import { FooterComponent } from './shared/footer';

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
                <a href="https://servicestack.net/vs-templates/Angular2App">Learn about this Angular2 VS.NET template</a>
            </h4>

            <app-footer></app-footer>

        </main>
    </div>
    `
})
export class AppComponent {
    ngAfterViewInit() {
        componentHandler.upgradeDom();
    }
}
