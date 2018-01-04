import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home';
import { View1Component } from './view1/view1';
import { View2Component } from './view2/view2';

import { JsonServiceClient } from '@servicestack/client';

export const routes: Routes = [
  {
      path: '',
      redirectTo: '/',
      pathMatch: 'full'
  },
  { path: '', component: HomeComponent, data: { title: 'Home', name: 'Angular 5' } },
  { path: 'view1', component: View1Component },
  { path: 'view2', component: View2Component },
  { path: '**', redirectTo: '/' },
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    View1Component,
    View2Component
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes)
  ],
  providers: [{provide:JsonServiceClient, useValue: new JsonServiceClient("/")}],
  bootstrap: [AppComponent]
})
export class AppModule { }
