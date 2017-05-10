import { Component, Injectable, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { routes } from '../modules/app/app.module';

@Component({
    selector: 'app-header',
    template: `
        <header class="mdl-layout__header mdl-layout__header--scroll mdl-color--primary">
            <div class="mdl-layout--large-screen-only mdl-layout__header-row"></div>
            <div class="mdl-layout--large-screen-only mdl-layout__header-row">
                <h3>{{title}}</h3>
            </div>
            <div class="mdl-layout--large-screen-only mdl-layout__header-row"></div>
            <div class="mdl-layout__tab-bar mdl-js-ripple-effect mdl-color--primary-dark">

                <span *ngFor="let route of routes">
                    <a class="mdl-layout__tab" [class.is-active]="isActive(route.path)"
                        routerLink="{{route.path}}">{{route.data.title}}</a>
                </span>

                <button class="mdl-button mdl-js-button mdl-button--fab mdl-js-ripple-effect mdl-button--colored mdl-shadow--4dp mdl-color--accent" id="add">
                    <i class="material-icons" role="presentation">add</i>
                    <span class="visuallyhidden">Add</span>
                </button>
            </div>
        </header>
    `
})
export class HeaderComponent {
    title = '$safeprojectname$';
    routes = routes.filter((val) => val.path != '');

    constructor(private route: ActivatedRoute, private router: Router) {}

    isActive(path: string): boolean {
        return this.router.url.substring(1) === path;
    }
}