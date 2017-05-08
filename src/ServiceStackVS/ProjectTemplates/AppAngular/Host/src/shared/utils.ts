import { Directive, AfterViewInit } from '@angular/core';
import { JsonServiceClient } from 'servicestack-client';

export var client = new JsonServiceClient('/');

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