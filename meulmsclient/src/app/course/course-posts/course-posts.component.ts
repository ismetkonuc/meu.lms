import { Component, Input, OnInit } from '@angular/core';
import { IArticle } from 'src/app/shared/models/IArticle';

@Component({
  selector: 'app-course-posts',
  templateUrl: './course-posts.component.html',
  styleUrls: ['./course-posts.component.css']
})
export class CoursePostsComponent implements OnInit {
  constructor() { }

  @Input() courseArticles: IArticle[] = [];
  

  ngOnInit(): void {
  }

}
