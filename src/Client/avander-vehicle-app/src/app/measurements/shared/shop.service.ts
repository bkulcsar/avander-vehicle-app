import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IShop } from '.';

@Injectable()
export class ShopService {
  url: string;
  constructor(private http: HttpClient) {
    this.url = '/api/shops';
  }
  getShops(): Observable<IShop[]> {
    return this.http
      .get<IShop[]>(this.url, {})
      .pipe(catchError(this.handleError<IShop[]>('getShops')));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.log('Error', error);
      return of(result as T);
    };
  }
}
