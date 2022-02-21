import { HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IEnvironment } from 'src/environments/ienvironment';
import { EnvironmentService } from './environment.service';


@Injectable({
  providedIn: 'root'
})
export class ApiInterceptorService implements HttpInterceptor {

  constructor(private environment: EnvironmentService) {}
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const request = req.clone({headers: req.headers.append('Production', this.environment.production.toString())})
    return next.handle(request);
  }
}
