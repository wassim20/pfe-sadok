import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SapComponent } from './sap.component';

describe('SapComponent', () => {
  let component: SapComponent;
  let fixture: ComponentFixture<SapComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SapComponent]
    });
    fixture = TestBed.createComponent(SapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
