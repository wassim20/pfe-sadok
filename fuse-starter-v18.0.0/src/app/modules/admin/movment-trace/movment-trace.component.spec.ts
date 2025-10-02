import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovmentTraceComponent } from './movment-trace.component';

describe('MovmentTraceComponent', () => {
  let component: MovmentTraceComponent;
  let fixture: ComponentFixture<MovmentTraceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MovmentTraceComponent]
    });
    fixture = TestBed.createComponent(MovmentTraceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
