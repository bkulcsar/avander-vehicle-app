import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IMeasurement } from '.';

@Injectable()
export class MeasurementService {
  constructor(private http: HttpClient) {}
  getMeasurements(): Observable<HttpResponse<IMeasurement[]>> {
    return this.http
      .get<IMeasurement[]>('/api/measurements?expand=true', {
        observe: 'response',
      })
      .pipe(
        catchError(
          this.handleError<HttpResponse<IMeasurement[]>>('getMeasurements')
        )
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.log('Error', error);
      return of(result as T);
    };
  }
}
