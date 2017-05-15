import { TestBed } from '@angular/core/testing';
import { provideRoutes } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';

import { HeaderComponent } from './shared/header';
import { FooterComponent } from './shared/footer';
import { AppComponent } from './app.component';

describe('App', () => {
  // provide our implementations or mocks to the dependency injector
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [HeaderComponent, FooterComponent, AppComponent],
      providers: [provideRoutes([])]
    });
  });

  it('Should have a URL', () => {
    let fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    expect(fixture.debugElement.componentInstance.url).toEqual('https://servicestack.net/vs-templates/AngularApp');
  });

});
