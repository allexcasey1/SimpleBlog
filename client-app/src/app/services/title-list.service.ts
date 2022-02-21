import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Title } from '../models/Title';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})

export class TitleListService extends DataService<Title> {

  constructor(http: HttpClient) { 
    super('http://localhost:5000/api/BlogPosts/', http)
  }
}
