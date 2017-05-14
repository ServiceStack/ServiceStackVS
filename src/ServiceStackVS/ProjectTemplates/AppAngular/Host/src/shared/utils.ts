import { Directive, AfterViewInit } from '@angular/core';
import { JsonServiceClient } from 'servicestack-client';

declare var global; //populated from package.json/karma/globals

export var client = new JsonServiceClient(global.BaseUrl || '/');


@Directive({
    selector: '[mdl]'
})
export class MDL implements AfterViewInit {
    ngAfterViewInit() {
        if (typeof componentHandler !== "undefined") {
            componentHandler.upgradeDom();
        }
    }
}