import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BlogFormComponent } from './features/blogs/blog-form/blog-form.component';
import { BlogPostComponent } from './features/blogs/blog-post/blog-post.component';
import { TitleListComponent } from './features/blogs/title-list/title-list.component';

const routes: Routes = [
  {path: 'home', component: TitleListComponent},
  {path: 'blog/:id', component: BlogPostComponent},
  {path: 'form', component: BlogFormComponent},
  {path: 'form/:id', component: BlogFormComponent},
  {path: '', redirectTo: '/home', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
