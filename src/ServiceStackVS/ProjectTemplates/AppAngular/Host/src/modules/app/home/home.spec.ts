// This shows a different way of testing a component, see technology for a simpler one
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { TestBed } from '@angular/core/testing';

import { HelloComponent } from './hello';
import { HomeComponent } from './home';

describe('Home Component', () => {
  const html = '<home></home>';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [HelloComponent, HomeComponent, TestComponent]
    });
    TestBed.overrideComponent(TestComponent, { set: { template: html }});
  });

  it ('Should render heading', () => {
    const fixture = TestBed.createComponent(TestComponent);
    fixture.detectChanges();
    expect(fixture.nativeElement.querySelector('h4').textContent).toContain('Home');
  });

});

@Component({selector: 'my-test', template: ''})
class TestComponent { }
