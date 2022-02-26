import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, Observable, pipe, tap, throwError } from 'rxjs';
import { AppError } from 'src/app/common/app-error';
import { AuthService } from 'src/app/services/auth.service';
import { User, UserForm, UserResponse } from '../../models/User';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})

export class AuthComponent implements OnInit {
  isLoginMode = true;
  isLoading: boolean = false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  onSwitchMode() {
    this.isLoginMode = !this.isLoginMode;
  }

  onSubmit(form: NgForm){
    this.isLoading = true;

    if (!form.valid) return;

    const email = form.value.email;
    const password = form.value.password;
    let authObs!: Observable<UserResponse>;

    if (!this.isLoginMode) {
      const displayName = form.value.displayName;
      const username = form.value.username;
      
      authObs = this.authService.signUp({ username: username, password: password, displayName: displayName, email: email} as UserForm);

    } else {

      authObs = this.authService.login({email: email, password: password} as UserForm);
    }

    authObs.subscribe(
      res => {
        console.log(res);
        this.isLoading = false;
        this.router.navigate(['/'])
      }
    )

    form.reset();
  }

  private handleError(error: any) { return throwError(() => new AppError(JSON.stringify(error))) };

}
