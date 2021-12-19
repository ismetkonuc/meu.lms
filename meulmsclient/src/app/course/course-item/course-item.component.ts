import { Component, Input, OnInit } from '@angular/core';
import { ICourse } from 'src/app/shared/models/ICourse';
import { CourseModule } from '../course.module';
@Component({
  selector: 'app-course-item',
  templateUrl: './course-item.component.html',
  styleUrls: ['./course-item.component.css']
})
export class CourseItemComponent implements OnInit {

  @Input() course:ICourse = {id: 0, name : '', code:''};

  centered = false;
  disabled = false;
  unbounded = false;

  radius: number = 0;
  color: string = '';

  courseName:string='';

  constructor() { }

  ngOnInit(): void {
  }

}
