import { TestBed } from '@angular/core/testing';

import { TechnologyComponent } from './technology';

describe('Technology Component', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TechnologyComponent]});
  });

  it ('Should render heading', () => {
    const fixture = TestBed.createComponent(TechnologyComponent);
    fixture.detectChanges();
    expect(fixture.nativeElement.querySelector('h4').textContent).toContain('Technology');
  });

});
