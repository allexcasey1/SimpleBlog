import { Component, OnInit } from '@angular/core';
import { Title } from 'src/app/models/Title';
import { TitleListService } from 'src/app/services/title-list.service';

@Component({
  selector: 'title-list',
  templateUrl: './title-list.component.html',
  styleUrls: ['./title-list.component.css']
})
export class TitleListComponent implements OnInit {
  public titleList! : Title[];

  constructor(private service: TitleListService) {}

  ngOnInit(): void {
    this.service.list().subscribe(res => {
      this.titleList = res;
    })
  }

}
