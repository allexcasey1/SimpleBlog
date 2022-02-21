import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BlogPost } from '../models/BlogPost';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})

export class BlogService extends DataService<BlogPost> {

  constructor(http: HttpClient) { 
    super("http://localhost:5000/api/BlogPosts", http)
  }
}
