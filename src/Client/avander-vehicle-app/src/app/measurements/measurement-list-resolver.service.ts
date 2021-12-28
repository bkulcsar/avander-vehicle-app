import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { MeasurementService } from './shared/measurement.service';

@Injectable()
export class MeasurementListResolver implements Resolve<any> {
  constructor(private measurementService: MeasurementService) {}

  resolve() {
    const response = this.measurementService.getMeasurements();
    return response;
  }
}
