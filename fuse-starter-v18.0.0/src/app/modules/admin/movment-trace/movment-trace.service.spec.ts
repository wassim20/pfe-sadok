import { TestBed } from '@angular/core/testing';

import { MovmentTraceService } from './movment-trace.service';

describe('MovmentTraceService', () => {
  let service: MovmentTraceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MovmentTraceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
