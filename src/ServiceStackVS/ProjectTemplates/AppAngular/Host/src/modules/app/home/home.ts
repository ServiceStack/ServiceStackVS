import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HelloComponent } from './hello';

@Component({
    selector: 'home',
    template: `
        <section class="section--center mdl-grid mdl-grid--no-spacing mdl-shadow--2dp">
            <header class="section__play-btn mdl-cell mdl-cell--3-col-desktop mdl-cell--2-col-tablet mdl-cell--4-col-phone mdl-color--teal-100 mdl-color-text--white">
                <i class="material-icons">play_circle_filled</i>
            </header>

            <div class="mdl-card mdl-cell mdl-cell--9-col-desktop mdl-cell--6-col-tablet mdl-cell--4-col-phone">

                <hello [name]="name"></hello> 

                <div class="mdl-card__actions">
                    <a href="http://docs.servicestack.net/typescript-add-servicestack-reference" class="mdl-button">More info on TypeScript ServiceStack Reference</a>
                </div>
            </div>
        </section>
    `
})
export class HomeComponent {
    name: string;
    heading: string;

    constructor(private route: ActivatedRoute) {
        this.heading = "Home";
    }

    ngOnInit() {
        this.route.data.subscribe(x => this.name = x.name);
    }
}