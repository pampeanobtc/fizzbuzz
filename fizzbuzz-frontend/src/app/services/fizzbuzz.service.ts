import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import {HttpClient} from '@angular/common/http';
import { MessageService } from './message.service';

interface ApiInput {
  input: number;
  max: number;
}

export interface ApiResult {
  value: string;
}

@Injectable({
  providedIn: 'root'
})
export class FizzBuzzService {

  apiUrl: string = "https://localhost:7045/api/v1/FizzBuzz";

  constructor(private http:HttpClient, private messageService: MessageService) {
  }

  processResultsOnApi(input: number, max: number) : Observable<ApiResult> {

    const payload: ApiInput = {
      input: input,
      max: max
    };

    return this.http.post<ApiResult>(this.apiUrl, payload)
      .pipe(catchError(this.handleError<ApiResult>('processResultsOnApi'))
      );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error);

      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    this.messageService.add(`FizzBuzzService: ${message}`);
  }
}
