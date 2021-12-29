import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { MeasurementService } from '.';

@Injectable()
export class MeasurementListResolver implements Resolve<any> {
  constructor(private measurementService: MeasurementService) {}

  resolve() {
    const response = this.measurementService.getMeasurements({ expand: true });
    return response;
  }
}
