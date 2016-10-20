import { Routes,Router } from '@angular/router';
import { HomeComponent } from './home/home';
import { View1Component } from './view1/view1';
import { View2Component } from './view2/view2';

export const appRoutes: Routes = [
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    { path: 'home', component: HomeComponent, data: { name: 'Home' } },
    { path: 'view1', component: View1Component, data: { name: 'View 1' } },
    { path: 'view2', component: View2Component, data: { name: 'View 2' } }
];