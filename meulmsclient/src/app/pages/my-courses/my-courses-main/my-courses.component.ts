import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { ICourseList } from 'src/app/shared/models/ICourseList';
import { ITask } from 'src/app/shared/models/ITask';
import { ITaskList } from 'src/app/shared/models/ITaskList';
import { MyCoursesService } from 'src/app/shared/services/my-courses.service';


interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

@Component({
  selector: 'app-my-courses',
  templateUrl: './my-courses.component.html',
  styleUrls: ['./my-courses.component.css']
})
export class MyCoursesComponent implements OnInit {
  courses: ICourseList[] = [];
  userRole:any;
  constructor(private myCoursesService:MyCoursesService) {
    this.userRole = localStorage.getItem('role');
   }

  ngOnInit(): void {
    this.loadMyCourses();
    // this.dataSource.data = this.courses;
  }

  getTaskId(id:number){
    // console.log(id)
    this.myCoursesService.getSpesificTask(id);
  }

  loadMyCourses() {
    this.myCoursesService.getMyCourses().subscribe(response => {
      this.courses = response;
      console.log(response)
    })
  }
}
