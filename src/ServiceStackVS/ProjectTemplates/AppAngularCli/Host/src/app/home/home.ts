/// <reference path="../../dtos.d.ts" />
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { createUrl } from 'servicestack-client';

@Component({
    templateUrl: 'home.html',
    styleUrls: ['home.scss']
})
export class HomeComponent implements OnInit {
    result: string;

    constructor(private route: ActivatedRoute, private http: HttpClient) {
    }

    @Input() name: string;

    ngOnInit() {
        this.route.data.subscribe(x => this.name = x.name);
        this.nameChanged(this.name);
    }

    nameChanged(name: string) {
        if (name) {
            this.http.get<HelloResponse>(createUrl('/hello/{Name}', { name })).subscribe(r => {
                this.result = r.result;
            });
        } else {
            this.result = '';
        }
    }
}
