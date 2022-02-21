import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, retry } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { AppError } from '../common/app-error';
 
@Injectable({
  providedIn: 'platform'
})

export class DataService<T>{

  constructor(@Inject(String) private url: string, private http: HttpClient) {}

  public create(entity: T) {
    return this.http.post(this.url.toString(), entity)
      .pipe(
        map(response => JSON.stringify(response)),
        retry(1),
        catchError(this.handleError)
      )
  };

  public list() : Observable<T[]> {
    return this.http.get<T[]>(this.url)
      .pipe(
        // map(response => response),
        retry(1),
        catchError(this.handleError)
      )
  };

  public getById(id: string | null) : Observable<T> {
    return this.http.get<T>(this.url + '/' + id)
      .pipe(
        // map(response => response),
        retry(1),
        catchError(this.handleError)
      )
  };

  public update<T extends {id? : string}>(entity: T) {
    return this.http.put(this.url +  "/" + entity.id, entity)
      .pipe(
        map(response => JSON.stringify(response)),
        retry(1),
        catchError(this.handleError)
      )
  };

  // creates generic application error
  // todo: extend generic error based on error.status and create switch clauses
  private handleError(error: any) { return throwError(() => new AppError(JSON.stringify(error))) };
  
}
