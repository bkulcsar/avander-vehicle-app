import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IMeasurement, IShop } from '.';
import { MeasurementService } from './shared/measurement.service';

@Component({
  selector: 'measurement-list',
  templateUrl: './measurement-list.component.html',
  styleUrls: ['./measurement-list.component.css'],
})
export class MeasurementListComponent implements OnInit {
  measurements: IMeasurement[] | null;
  shops: IShop[] | null;
  pageNumbers: number[];
  pageSize: number;
  activePage: number;
  jsnFilter: string | undefined;
  measurementPointFilter: string | undefined;
  fromDateFilter: Date | undefined;
  toDateFilter: Date | undefined;
  shopFilter: string | undefined;

  constructor(
    private measurementService: MeasurementService,
    private route: ActivatedRoute
  ) {
    this.measurements = [];
    this.shops = [];
    this.pageNumbers = [1];
    this.activePage = 1;
    this.pageSize = 50;
  }

  onPageClick(pageNumber: number) {
    this.measurementService
      .getMeasurements({
        expand: true,
        page: pageNumber,
        size: this.pageSize,
      })
      .subscribe((response) => {
        this.measurements = response.body;
      });

    this.activePage = pageNumber;
  }

  onApplyFilterClick() {
    console.log(this.shopFilter);
    this.activePage = 1;
    this.measurementService
      .getMeasurements({
        expand: true,
        page: this.activePage,
        size: this.pageSize,
        jsn: this.jsnFilter,
        measurementPoint: this.measurementPointFilter,
        fromDate: this.fromDateFilter,
        toDate: this.toDateFilter,
        shop: this.shopFilter,
      })
      .subscribe((response) => {
        this.measurements = response.body;
        this.setPageCountFromHeader(response.headers);
      });
  }

  onClearFilterClick() {
    this.activePage = 1;
    this.measurementService
      .getMeasurements({
        expand: true,
        page: this.activePage,
        size: this.pageSize,
      })
      .subscribe((response) => {
        this.measurements = response.body;
        this.setPageCountFromHeader(response.headers);
      });
    this.jsnFilter = undefined;
    this.measurementPointFilter = undefined;
    this.toDateFilter = undefined;
    this.fromDateFilter = undefined;
    this.shopFilter = undefined;
  }

  isPageActive(pageNumber: number) {
    if (pageNumber === this.activePage) {
      return true;
    } else {
      return false;
    }
  }

  setPageCountFromHeader(headers: any) {
    const totalCount = headers.get('X-Total-Count');
    if (totalCount) {
      let pageNumber = Math.max(Math.ceil(totalCount / this.pageSize), 1);
      this.pageNumbers = Array.from(Array(pageNumber), (x, i) => i + 1);
    }
  }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      const response = data.measurementsResponse;
      this.measurements = response.body;
      this.setPageCountFromHeader(response.headers);

      this.shops = data.shops;
    });
  }
}
