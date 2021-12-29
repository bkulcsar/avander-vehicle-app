import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IMeasurement } from '.';

@Injectable()
export class MeasurementService {
  url: string;

  constructor(private http: HttpClient) {
    this.url = '/api/measurements';
  }

  getMeasurements(filter?: IFilter): Observable<HttpResponse<IMeasurement[]>> {
    let filterAdded = false;
    let queryParameters = '';

    if (filter) {
      let key: keyof typeof filter;
      for (key in filter) {
        if (Object.hasOwnProperty.call(filter, key)) {
          const value = filter[key];
          if (value) {
            if (filterAdded) {
              queryParameters = `${queryParameters}&${key}=${value}`;
            } else {
              queryParameters = `?${key}=${value}`;
              filterAdded = true;
            }
          }
        }
      }
    }

    return this.http
      .get<IMeasurement[]>(this.url + queryParameters, {
        observe: 'response',
      })
      .pipe(
        catchError(
          this.handleError<HttpResponse<IMeasurement[]>>('getMeasurements')
        )
      );
  }

  uploadMeasurements(files: any): Observable<HttpResponse<any>> {
    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    return this.http
      .post<HttpResponse<any>>(this.url + '/upload', formData, {
        observe: 'response',
      })
      .pipe(
        catchError(this.handleError<HttpResponse<any>>('uploadMeasurements'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.log('Error', error);
      return of(error);
    };
  }
}

export interface IFilter {
  page?: number;
  size?: number;
  expand?: boolean;
  jsn?: string;
  measurementPoint?: string;
  fromDate?: Date;
  toDate?: Date;
  shop?: string;
}
