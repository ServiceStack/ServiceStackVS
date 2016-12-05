import { Component } from '@angular/core';

@Component({
    selector: 'technology',
    template: `
        <section class="section--center mdl-grid mdl-grid--no-spacing mdl-shadow--2dp">
            <div class="mdl-card mdl-cell mdl-cell--12-col">
                <div class="mdl-card__supporting-text">
                    <h4>{{heading}}</h4>
                    Dolore ex deserunt aute fugiat aute nulla ea sunt aliqua nisi cupidatat eu. 
                    Nostrud in laboris labore nisi amet do dolor eu fugiat consectetur elit cillum esse. 
                    Pariatur occaecat nisi laboris tempor laboris eiusmod qui id Lorem esse commodo in. 
                    Exercitation aute dolore deserunt culpa consequat elit labore incididunt elit anim.
                </div>
                <div class="mdl-card__actions">
                    <a href="#" class="mdl-button">Read our features</a>
                </div>
            </div>
        </section>
    `
})
export class TechnologyComponent {
    heading: string;

    constructor() {
        this.heading = "Technology";
    }
}