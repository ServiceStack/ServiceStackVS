/// <reference path='../typings/index.d.ts'/>
import { Component } from '@angular/core';
import { appRoutes } from './app.routing';
import { RouterLink, Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'app-root',
    template: `
        <div class="navbar navbar-inverse" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#/">{{title}}</a>
                    <ul class="nav navbar-nav">
                        <li *ngFor="let route of routes">
                            <a routerLink="{{route.path}}">{{route.data.name}}</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="container">
            <router-outlet></router-outlet>
        </div>
    `,
    directives: [RouterLink]
})
export class AppComponent {
    title = 'Angular2App1';
    routes = appRoutes.filter((val) => val.path != '');
}
