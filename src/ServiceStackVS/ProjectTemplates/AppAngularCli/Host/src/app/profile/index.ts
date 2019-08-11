import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { IAuthSession } from '@servicestack/client';
import { StoreService } from '../shared/store';
import { Router } from '@angular/router';
import { Routes } from '../shared';

@Component({
    template: `
    <div *ngIf="userSession" id="profile" class="text-center">
        <img [src]="domSanitizer.bypassSecurityTrustUrl(userSession.profileUrl)" class="svg-8x">

        <p class="my-2">
            {{userSession.displayName}} <span *ngIf="userSession.userId">#{{userSession.userId}}</span>
        </p>
        <p>
            {{userSession.userName}}
        </p>
        <p *ngIf="userSession && userSession.roles" class="roles">
            <mark *ngFor="let x of userSession.roles">{{x}}</mark>
        </p>
        <p *ngIf="userSession && userSession.permissions" class="permissions">
            <mark *ngFor="let x of userSession.permissions">{{x}}</mark>
        </p>
        <p>
            <link-button (click)="onSignOut()" primary>Sign Out</link-button>
        </p>
    </div>
  `
})
export class ProfileComponent implements OnInit {

    userSession:IAuthSession = null;
  
    constructor(private store: StoreService, private domSanitizer:DomSanitizer, private router: Router) { }
   
    ngOnInit() {
      this.store.userSession.subscribe(x => this.userSession = x);
    }
    
    async onSignOut() { 
        await this.store.signOut();
        this.router.navigate([Routes.SignIn], { queryParams: { redirect: Routes.Profile } });
    }
}
