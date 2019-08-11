import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IAuthSession } from '@servicestack/client';
import { StoreService } from '../shared/store';

@Component({
    templateUrl: 'home.html',
})
export class HomeComponent implements OnInit {

    @Input() name: string;

    userSession:IAuthSession = null;

    constructor(private route: ActivatedRoute, private store: StoreService) {}
    
    async onSignOut() { 
        await this.store.signOut();
    }

    ngOnInit() {
        this.store.userSession.subscribe(x => this.userSession = x);
        this.route.data.subscribe(x => this.name = x.name);
    }
}
