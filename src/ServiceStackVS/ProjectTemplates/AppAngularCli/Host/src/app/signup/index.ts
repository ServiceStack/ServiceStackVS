import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { JsonServiceClient, toPascalCase, splitOnFirst, classNames } from '@servicestack/client';
import { StoreService } from '../shared/store';
import { Register } from '../shared/dtos';
import { Routes } from '../shared';

@Component({
    template: `
    <div class="row">
        <div class="col-4">
            <h3>Register New User</h3>

            <form (ngSubmit)="submit()" [className]="cls({ error:responseStatus, loading:loading })">
                <div class="form-group">
                    <error-summary except="displayName,email,password,confirmPassword" [responseStatus]="responseStatus"></error-summary>
                </div>
                <div class="form-group">
                    <ng-input name="displayName" [(ngModel)]="displayName" [responseStatus]="responseStatus"
                              placeholder="Display Name" label="Name" help="Your first and last name"></ng-input>
                </div>
                <div class="form-group">
                    <ng-input name="email" [(ngModel)]="email" [responseStatus]="responseStatus"
                              placeholder="Email" label="Email"></ng-input>
                </div>
                <div class="form-group">
                    <ng-input type="password" name="password" [(ngModel)]="password" [responseStatus]="responseStatus"
                              placeholder="Password" label="Password"></ng-input>
                </div>
                <div class="form-group">
                    <ng-input type="password" name="confirmPassword" [(ngModel)]="confirmPassword" [responseStatus]="responseStatus"
                              placeholder="Confirm" label="Confirm Password"></ng-input>
                </div>
                <div class="form-group">
                    <ng-checkbox name="autoLogin" [(ngModel)]="autoLogin" [responseStatus]="responseStatus">
                        Auto Login
                    </ng-checkbox>
                </div>
                <div class="form-group">
                    <ng-button type="submit" lg primary>Register</ng-button>
                    <link-button href="/signin" navItemClass="btn">Sign In</link-button>
                </div>

                <div class="pt-3">
                    <h5>Quick Populate:</h5>
                    <p class="pt-1">
                        <link-button outline-info sm (click)="newUser('new@user.com')">new@user.com</link-button>
                    </p>
                </div>
            </form>
        </div>
    </div>
    `
})
export class SignUpComponent implements OnInit {

    displayName = '';
    email = '';
    userName = '';
    password = '';
    confirmPassword = '';
    autoLogin = true;
    loading = false;
    responseStatus = null;

    constructor(private router: Router, private route: ActivatedRoute, private store: StoreService, private client: JsonServiceClient) {}

    ngOnInit() {
    }

    async submit() {
        try {
            this.loading = true;
            this.responseStatus = null;

            const response = await this.client.post(new Register({
                displayName: this.displayName,
                email: this.email,
                password: this.password,
                confirmPassword: this.confirmPassword,
                autoLogin: this.autoLogin,
            }));

            const isAuthenticated = await this.store.checkAuth();

            this.router.navigateByUrl(this.route.snapshot.queryParams.redirect || isAuthenticated ? Routes.Home : Routes.SignIn);

        } catch (e) {
            this.responseStatus = e.responseStatus || e;
        } finally {
            this.loading = false;
        }
    }

    newUser(email: string) {
        const names = email.split('@');
        this.displayName = toPascalCase(names[0]) + ' ' + toPascalCase(splitOnFirst(names[1], '.')[0]);
        this.email = email;
        this.password = this.confirmPassword = 'p@55wOrd';
    }

    cls(...args:any[]) { return classNames(args); }
}
