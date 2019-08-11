import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injectable } from '@angular/core';
import { RouterModule, Router, Routes as NgRoutes, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JsonServiceClient } from '@servicestack/client';
import { ServiceStackModule, ForbiddenComponent } from '@servicestack/angular';

import { AppComponent } from './app.component';
import { HomeComponent } from './home';
import { HelloApiComponent } from './home/HelloApi';
import { AboutComponent } from './about';
import { SignInComponent } from './signin';
import { SignUpComponent } from './signup';
import { ProfileComponent } from './profile';
import { AdminComponent } from './admin';

import { StoreService } from './shared/store';
import { Routes } from './shared';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private store: StoreService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const userSession = this.store.userSession.getValue();
    if (userSession == null) {
      this.router.navigate([Routes.SignIn], { queryParams: { redirect: state.url } });
      return false;
    }
    const role = route.data.role;
    if (role && (!userSession.roles || userSession.roles.indexOf(role) < 0)) {
      this.router.navigate([Routes.Forbidden], { state: { path: state.url, role } });
      return false;
    }
    const permission = route.data.permission;
    if (permission && (!userSession.permissions || userSession.permissions.indexOf(permission) < 0)) {
      this.router.navigate([Routes.Forbidden], { state: { path: state.url, permission } });
      return false;
    }
    return true;
  }
}

export const routes: NgRoutes = [
  {
      path: '',
      redirectTo: '/',
      pathMatch: 'full'
  },
  { path: '', component: HomeComponent, data: { title: 'Home', name: 'Angular 7' } },
  { path: 'about', component: AboutComponent, data: { message: 'About page' } },
  { path: 'signin', component: SignInComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'profile', component: ProfileComponent, canActivate:[AuthGuard] },
  { path: 'admin', component: AdminComponent, canActivate:[AuthGuard], data: { role: 'Admin' } },
  { path: 'forbidden', component: ForbiddenComponent },
  { path: '**', redirectTo: '/' },
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HelloApiComponent,
    AboutComponent,
    SignInComponent,
    SignUpComponent,
    ProfileComponent,
    AdminComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ServiceStackModule,
    RouterModule.forRoot(routes)
  ],
  providers: [{provide: JsonServiceClient, useValue: new JsonServiceClient('/')}],
  bootstrap: [AppComponent]
})
export class AppModule { }
