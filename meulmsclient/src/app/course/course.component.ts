import { Component, OnInit } from '@angular/core';
import { ICourse } from '../shared/models/ICourse';
import { CourseService } from './course.service';
@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  courses: ICourse[] = [];
  course:ICourse = {id:0, name:'', code:''}
  constructor(private courseService: CourseService) { }


  ngOnInit(): void {
    this.courseService.getCourses().subscribe(response => {
      this.courses = response;
    },
    error =>{
      console.log(error);
    });
  }

}
