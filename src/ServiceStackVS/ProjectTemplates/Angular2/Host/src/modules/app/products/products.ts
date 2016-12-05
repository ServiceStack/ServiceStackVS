import { Component } from '@angular/core';

@Component({
    selector: 'products', 
    template: `
        <section class="section--center mdl-grid mdl-grid--no-spacing mdl-shadow--2dp">
            <div class="mdl-card mdl-cell mdl-cell--12-col">
                <div class="mdl-card__supporting-text mdl-grid mdl-grid--no-spacing">
                <h4 class="mdl-cell mdl-cell--12-col">{{heading}}</h4>
                <div class="section__circle-container mdl-cell mdl-cell--2-col mdl-cell--1-col-phone">
                    <div class="section__circle-container__circle mdl-color--primary"></div>
                </div>
                <div class="section__text mdl-cell mdl-cell--10-col-desktop mdl-cell--6-col-tablet mdl-cell--3-col-phone">
                    <h5>Lorem ipsum dolor sit amet</h5>
                    Dolore ex deserunt aute fugiat aute nulla ea sunt aliqua nisi cupidatat eu. Duis nulla tempor do aute et eiusmod velit exercitation nostrud quis <a href="#">proident minim</a>.
                </div>
                <div class="section__circle-container mdl-cell mdl-cell--2-col mdl-cell--1-col-phone">
                    <div class="section__circle-container__circle mdl-color--primary"></div>
                </div>
                <div class="section__text mdl-cell mdl-cell--10-col-desktop mdl-cell--6-col-tablet mdl-cell--3-col-phone">
                    <h5>Lorem ipsum dolor sit amet</h5>
                    Dolore ex deserunt aute fugiat aute nulla ea sunt aliqua nisi cupidatat eu. Duis nulla tempor do aute et eiusmod velit exercitation nostrud quis <a href="#">proident minim</a>.
                </div>
                <div class="section__circle-container mdl-cell mdl-cell--2-col mdl-cell--1-col-phone">
                    <div class="section__circle-container__circle mdl-color--primary"></div>
                </div>
                <div class="section__text mdl-cell mdl-cell--10-col-desktop mdl-cell--6-col-tablet mdl-cell--3-col-phone">
                    <h5>Lorem ipsum dolor sit amet</h5>
                    Dolore ex deserunt aute fugiat aute nulla ea sunt aliqua nisi cupidatat eu. Duis nulla tempor do aute et eiusmod velit exercitation nostrud quis <a href="#">proident minim</a>.
                </div>
                </div>
                <div class="mdl-card__actions">
                <a href="#" class="mdl-button">Read our features</a>
                </div>
            </div>
        </section>
    `
})
export class ProductsComponent {
    heading: string;

    constructor() {
        this.heading = "Products";
    }
}