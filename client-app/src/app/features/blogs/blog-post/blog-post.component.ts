import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPost } from 'src/app/models/BlogPost';
import { BlogService } from 'src/app/services/blog.service';

@Component({
  selector: 'app-blog-post',
  templateUrl: './blog-post.component.html',
  styleUrls: ['./blog-post.component.css']
})
export class BlogPostComponent implements OnInit {
  private id!: string | null;
  public blogPost!: BlogPost;
  public isFetching: boolean = false;

  constructor(private service: BlogService, private route: ActivatedRoute, private router: Router) {
    
  }

  ngOnInit(): void {
    this.isFetching = true;
    this.id = this.route.snapshot.params['id'];
    this.fetchPost();
  }

  fetchPost() : void {
    this.service.getById(this.id)
      .subscribe(response => {
        this.blogPost = response;
        this.isFetching = false;
    });
  }

  editBlog() : void {
    this.router.navigate(['/form/' + this.id]);
  }

}
