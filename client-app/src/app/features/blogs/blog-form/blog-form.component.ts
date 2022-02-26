import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription, tap } from 'rxjs';
import { AppError } from 'src/app/common/app-error';
import { BlogPost } from 'src/app/models/BlogPost';
import { BlogService } from 'src/app/services/blog.service';
import { v4 as uuid } from 'uuid';

@Component({
  selector: 'app-blog-form',
  templateUrl: './blog-form.component.html',
  styleUrls: ['./blog-form.component.css']
})
export class BlogFormComponent implements OnInit{
  blogPost$!: Observable<BlogPost> | null;
  id: string | null = null;
  isFetching: boolean = false;
  editMode: boolean = false;
  form!: FormGroup;
 
  constructor(
    private service: BlogService, 
    private route: ActivatedRoute, 
    private fb: FormBuilder,
    private router: Router) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.editMode = !!this.id; 

    // create form in edit mode
    if (!!this.editMode) {
      this.isFetching = true;
      this.fetchPostAndUpdateForm()
      this.isFetching = false;
    } 

    // dynamically create form based on editMode value
    this.form = !!this.blogPost$ ? this.fb.group({
      id: [""],
      title: [""],
      content: this.fb.group({
        id: [""],
        sections: this.fb.array([])
      })
    }) : this.fb.group({
      id: [uuid()],
      title: [''], 
      content: this.fb.group({
        id: [uuid()],
        sections: this.fb.array([])
      })
    });

    // add blank section if creating new post
    if (this.blogPost$ == null) {
      this.addNewSection();
    }
  }

  // getters and setters
  get blogContent(): FormGroup {
    return this.form.get('content') as FormGroup;
  }

  get blogSections(): FormArray {
    return this.blogContent.get('sections') as FormArray;
  }

  // helper functions
  addNewSection() {
    this.blogSections.push(
      this.fb.group({
        id: [uuid()],
        sectionHeader: [''],
        sectionText: ['']
      })
    );
  }

  addExistingSection(id: string, header: string, text: string) {
    this.blogSections.push(
      this.fb.group({
        id: [id],
        sectionHeader: [header],
        sectionText: [text]
      })
    );
  }

  deleteSection(index: number) {
    this.blogSections.removeAt(index);
  }

  fetchPostAndUpdateForm() {
    this.blogPost$ = this.service.getById(this.id)
      .pipe(
        tap(post => {
          this.form.patchValue(post)
          post.content.sections.forEach(section => {
            this.addExistingSection(section.id, section.sectionHeader, section.sectionText)
        })}));
  }
  
  onSubmit() {
    console.log(this.form.value);
    let blogPost = this.form.value as BlogPost;
    let posting!: Subscription;
    if(!!this.editMode) {
      posting = this.service.update(blogPost)
        .subscribe(() => {
          this.router.navigate(['/blog/' + blogPost.id], {relativeTo: this.route});
        });
    } else {
      posting = this.service.create(blogPost)
        .subscribe(() => {
          this.router.navigate(['/blog/' + blogPost.id], {relativeTo: this.route});
        });
    }
  }

  cancel() {
    if (this.id != null ) {
      this.router.navigate(['/blog/' + this.id]);
    } else {
      this.router.navigate(['/'])
    }
  }
  

}
