import { NgModule } from '@angular/core';
import { BrowserModule, BrowserPlatformLocation } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home';
import { View1Component } from './view1/view1';
import { View2Component } from './view2/view2';
import { RouterModule, Router } from '@angular/router';
import { bootstrap } from '@angular/platform-browser-dynamic';
import { AppComponent } from './app';
import {
    appRoutes
} from './app.routing';
import {
    LocationStrategy,
    HashLocationStrategy
} from '@angular/common';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule.forRoot(appRoutes)
    ],
    providers: [{
        provide: LocationStrategy,
        useClass: HashLocationStrategy
    }],
    declarations: [
        AppComponent,
        HomeComponent,
        View1Component,
        View2Component
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}