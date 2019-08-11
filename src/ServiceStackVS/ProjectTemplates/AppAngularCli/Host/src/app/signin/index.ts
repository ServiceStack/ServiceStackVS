import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { IAuthSession, JsonServiceClient, GetNavItemsResponse, classNames } from '@servicestack/client';
import { StoreService } from '../shared/store';
import { Authenticate } from '../shared/dtos';
import { Routes } from '../shared';

@Component({
    template: `
    <div class="row">
        <div class="col-4">
            <h3>Sign In</h3>
            
            <form (ngSubmit)="submit()" [className]="cls({ error:responseStatus, loading:loading })">
                <div class="form-group">
                    <error-summary except="userName,password" [responseStatus]="responseStatus"></error-summary>
                </div>
                <div class="form-group">
                    <ng-input name="userName" [(ngModel)]="userName" placeholder="Username" [responseStatus]="responseStatus" 
                              label="Email" help="Email you signed up with"></ng-input>
                </div>
                <div class="form-group">
                    <ng-input type="password" name="password" [(ngModel)]="password" placeholder="Password" [responseStatus]="responseStatus" 
                              label="Password" help="6 characters or more"></ng-input>
                </div>
                <div class="form-group">
                    <ng-checkbox name="rememberMe" [(ngModel)]="rememberMe" [responseStatus]="responseStatus">
                        Remember Me
                    </ng-checkbox>
                </div>
                <div class="form-group">
                    <button type="submit" class="btn btn-lg btn-primary">Login</button>
                    <link-button href="/signup" lg outline-secondary class="ml-2">Register New User</link-button>
                </div>
            </form>
            
            <div class="pt-3">
                <h5>Quick Login:</h5>
                <p class="btn-group">
                    <link-button outline-info sm (click)="switchUser('admin@email.com')">admin@email.com</link-button>
                    <link-button outline-info sm (click)="switchUser('new@user.com')">new@user.com</link-button>
                </p>
            </div>
        </div>

        <div class="col-5">
            <div class="row justify-content-end mt-5">
                <div class="col col-8">
                    <nav-button-group [items]="nav.navItemsMap.auth" [attributes]="userAttributes" [baseHref]="nav.baseUrl" block lg></nav-button-group>
                </div>
            </div>
        </div>
    </div>
    `
})
export class SignInComponent implements OnInit {

    userName = '';
    password = '';
    rememberMe = true;
    loading = false;
    responseStatus = null;

    userSession:IAuthSession = null;

    nav:GetNavItemsResponse;
    userAttributes:string[];
    baseUrl:string;

    constructor(private router: Router, private route: ActivatedRoute, private store: StoreService, private client: JsonServiceClient) {}

    ngOnInit() {
        if (this.store.userSession.getValue() != null) {
            this.router.navigateByUrl(this.route.snapshot.queryParams.redirect || Routes.Home);
        }
        this.store.nav.subscribe(x => this.nav = x);
        this.store.userAttributes.subscribe(x => this.userAttributes = x);
    }

    async submit() {
        try {
            this.loading = true;
            this.responseStatus = null;

            const response = await this.client.post(new Authenticate({
                provider: 'credentials',
                userName: this.userName,
                password: this.password,
                rememberMe: this.rememberMe,
            }));
            this.store.signIn(response);

            this.router.navigateByUrl(this.route.snapshot.queryParams.redirect || Routes.Home);

        } catch (e) {
            console.log('submit', this);
            this.responseStatus = e.responseStatus || e;
        } finally {
            this.loading = false;
        }
    }

    switchUser(email: string) {
        this.userName = email;
        this.password = 'p@55wOrd';
    }

    cls(...args:any[]) { return classNames(args); }
}
