import { HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take, exhaustMap } from 'rxjs';
import { AuthService } from './auth.service';


@Injectable()

export class ApiInterceptorService implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
      return this.authService.user.pipe(
          take(1),
          exhaustMap(user => {
            let modifiedRequest!: HttpRequest<any>;
            if (user != null) {
              modifiedRequest = req.clone({headers: req.headers.append('Authorization', 'Bearer ' + user!.token)});
            } else {
              modifiedRequest = req.clone();
            }
            return next.handle(modifiedRequest);
          })
        );

    
    // const request = req.clone({headers: req.headers.append('Production', this.environment.production.toString())})
    
  }
}
