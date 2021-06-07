/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RAN_ChartsService } from './RAN_Charts.service';

describe('Service: RAN_Charts', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RAN_ChartsService]
    });
  });

  it('should ...', inject([RAN_ChartsService], (service: RAN_ChartsService) => {
    expect(service).toBeTruthy();
  }));
});
