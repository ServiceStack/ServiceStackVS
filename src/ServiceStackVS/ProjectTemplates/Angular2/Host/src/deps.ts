/// <reference path='../typings/index.d.ts'/>
import { NgModule } from '@angular/core';
import { BrowserModule, platformBrowser } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { } from '@angular/compiler';


export class Deps {
    constructor() {
        var ignore1 = NgModule;
        var ignore2 = BrowserModule;
        var ignore3 = platformBrowser;
        var ignore4 = FormsModule;
        var ignore5 = RouterModule;
        var ignore6 = Router;
        var ignore7 = platformBrowserDynamic;
    }
}