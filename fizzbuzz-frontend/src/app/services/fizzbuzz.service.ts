import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {MatSnackBar} from '@angular/material/snack-bar';

import {HttpClient, HttpErrorResponse} from '@angular/common/http';

interface ApiInput {
  randomNumberToStart: number;
  limit: number;
}

export interface ApiResult {
  items: string[];
  signature: number
}

@Injectable({
  providedIn: 'root'
})
export class FizzBuzzService {

  // this should be configurable
  apiUrl: string = "https://localhost:7045/api/v1/FizzBuzz";
  maxNumber: number = 100;

  constructor(private http:HttpClient, readonly snackBar: MatSnackBar) {
  }

  getRandomizedResults() : Observable<ApiResult> {
    const randomNumberToStart = parseInt(((Math.random() * (this.maxNumber - 1))).toFixed(0));
    const limit = parseInt(((Math.random() * (this.maxNumber))).toFixed(0));
    return this.processResultsOnApi(randomNumberToStart, limit);
  }

  processResultsOnApi(randomNumberToStart: number, limit: number) : Observable<ApiResult> {

    const payload: ApiInput = {
      randomNumberToStart: randomNumberToStart,
      limit: limit
    };

    return this.http.post<ApiResult>(this.apiUrl, payload)
      .pipe(catchError(this.handleError<ApiResult>('processResultsOnApi')));
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: HttpErrorResponse): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error);

      this.snackBar.open(error.message, "Close", { duration: 5000 });

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

}
