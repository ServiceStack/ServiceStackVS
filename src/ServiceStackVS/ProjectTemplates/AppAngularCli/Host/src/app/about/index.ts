import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    template: `
    <div id="about">
        <div class="svg-users svg-8x ml-2"></div>
        <h3>{{message}}</h3>
    </div>
    `
})
export class AboutComponent implements OnInit {
    message = '';

    constructor(private route: ActivatedRoute) {}

    ngOnInit() {
        this.route.data.subscribe(x => this.message = x.message);
    }
}
