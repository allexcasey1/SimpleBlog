import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AppError } from '../common/app-error';
import { User, UserForm, UserResponse } from '../models/User';
import { EnvironmentService } from './environment.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  user = new BehaviorSubject<User>(null!);

  constructor(private http: HttpClient, private environment: EnvironmentService) { }

  signUp(user: UserForm) {
    return this.http.post<UserResponse>(
      `${this.environment.apiHost}${this.environment.apiUrl}` + 'account/register', user
    ).pipe(
      catchError(this.handleError),
      tap(res => {
        this.handleAuthentication(res.email, res.username, res.displayName, res.token, res.expires);
      }),
    )
  }

  login(user: UserForm) {
    return this.http.post<UserResponse>(
      `${this.environment.apiHost}${this.environment.apiUrl}` + 'account/login', user
    ).pipe(
      catchError(this.handleError),
      tap(res => {
        this.handleAuthentication(res.email, res.username, res.displayName, res.token, res.expires);
      })
    )
  }

  private handleAuthentication(email: string, username: string, displayName: string, token: string, expires: Date) {
    const user = new User(email, username, displayName, token, expires);
    this.user.next(user);
  }

  // creates generic application error
  // todo: extend generic error based on error.status and create switch clauses
  private handleError(error: any) { return throwError(() => new AppError(JSON.stringify(error))) };
}
