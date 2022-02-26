import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '../environments/environment';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppErrorHandler } from './common/app-error-handler';
import { TitleListComponent } from './features/blogs/title-list/title-list.component';
import { BlogPostComponent } from './features/blogs/blog-post/blog-post.component';
import { BlogFormComponent } from './features/blogs/blog-form/blog-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ApiInterceptorService } from './services/api-interceptor.service';
import { AuthComponent } from './features/auth/auth.component';
import { HeaderComponent } from './features/layout/header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    TitleListComponent,
    BlogPostComponent,
    BlogFormComponent,
    AuthComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    StoreModule.forRoot({}, {}),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production })
  ],
  providers: [
    { 
      provide : ErrorHandler, 
      useClass: AppErrorHandler
    },
    { 
      provide: HTTP_INTERCEPTORS, 
      useClass: ApiInterceptorService, 
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
