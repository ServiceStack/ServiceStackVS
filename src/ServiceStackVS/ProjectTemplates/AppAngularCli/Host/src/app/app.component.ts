import { Component, OnInit } from '@angular/core';
import { StoreService } from './shared/store';
import { GetNavItemsResponse } from '@servicestack/client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'app';

  nav:GetNavItemsResponse = null;
  userAttributes:string[] = [];

  constructor(private store: StoreService) { }
 
  ngOnInit() {
    this.store.nav.subscribe(x => this.nav = x);
    this.store.userAttributes.subscribe(x => this.userAttributes = x);
  }
}
