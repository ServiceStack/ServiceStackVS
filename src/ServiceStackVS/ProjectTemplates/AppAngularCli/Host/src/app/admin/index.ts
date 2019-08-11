import { Component, OnInit } from '@angular/core';
import { IAuthSession } from '@servicestack/client';
import { StoreService } from '../shared/store';

@Component({
    template: `
        <div *ngIf="userSession" id="admin" class="text-center">
            <div class="svg-female-business svg-8x ml-2"></div>
            <p class="my-2">
                {{userSession.displayName}}
            </p>
            <p>
                {{userSession.userName}}
            </p>
            <p *ngIf="userSession && userSession.roles" class="roles">
                <mark *ngFor="let x of userSession.roles">{{x}}</mark>
            </p>
            <h3 class="mt-5">Admin Page</h3>
        </div>
    `
})
export class AdminComponent implements OnInit {

    userSession:IAuthSession = null;
  
    constructor(private store: StoreService) { }
   
    ngOnInit() {
      this.store.userSession.subscribe(x => this.userSession = x);
    }    
}
