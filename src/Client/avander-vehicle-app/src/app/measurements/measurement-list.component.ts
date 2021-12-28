import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IMeasurement } from '.';
import { MeasurementService } from './shared/measurement.service';

@Component({
  selector: 'measurement-list',
  templateUrl: './measurement-list.component.html',
  styleUrls: ['./measurement-list.component.css'],
})
export class MeasurementListComponent implements OnInit {
  measurements: IMeasurement[];
  pageNumber: number[];
  pageSize: number;
  constructor(
    private measurementService: MeasurementService,
    private route: ActivatedRoute
  ) {
    this.measurements = [];
    this.pageNumber = [1];
    this.pageSize = 50;
  }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      const response = data.measurementsResponse;
      this.measurements = response.body;
      const totalCount = response.headers.get('X-Total-Count');

      if (totalCount) {
        let pageNumber = Math.ceil(totalCount / this.pageSize);
        this.pageNumber = Array.from(Array(pageNumber), (x, i) => i + 1);
      }
    });
  }
}
