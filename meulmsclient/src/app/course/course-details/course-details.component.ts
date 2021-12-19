import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IAssignment } from 'src/app/shared/models/IAssignment';
import { ITask } from 'src/app/shared/models/ITask';
import { CourseService } from '../course.service';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrls: ['./course-details.component.css']
})
export class CourseDetailsComponent implements OnInit {
  @ViewChild('fileInput')
  fileInput: any;

  file: any;
  courseTasks: ITask[] = [];
  userAssignments: IAssignment[] = [];
  activatedRouteId: number;
  courseName : string = '';
  isItUpdate : boolean = false;
  handledTaskId: number = 0;
  
  constructor(private courseService : CourseService, private activatedRoute:ActivatedRoute, private http:  HttpClient) {
    this.activatedRouteId = Number(this.activatedRoute.snapshot.paramMap.get('id'))
   }


  ngOnInit(): void {
    this.loadCourse();
    // this.getCurrentUserAssignments();
  }

  async loadCourse(){

    this.courseName  = await this.courseService.getCourseName(this.activatedRouteId)
  
    this.courseService.getTasks(this.activatedRouteId).subscribe(courseTasks => {
      this.courseTasks = courseTasks;
    },
    error=>{
      console.log(error)
    })


  }

  // getCurrentUserAssignments(){
  //   this.courseService.getUserAssignments(this.activatedRouteId).subscribe(response => {
  //     console.log(response)
  //     this.userAssignments = response;
  //   });
  // }


  onChangeFileInput() {
    
    const files: { [key: string]: File } = this.fileInput.nativeElement.files;
    this.file = files[0];

    const formData: FormData = new FormData();
    formData.append('Attachment', this.file);
    formData.append('CourseId', "1");
    formData.append('TaskId', this.handledTaskId.toString());
    formData.append('AppUserId', "0");


    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)

    return this.http.post('http://localhost:5000/api/assignments', formData,
    {
      headers : headers})
    .subscribe(() => this.ngOnInit());

  }

  onClickFileInputButton(value:any, id:any): void {
    this.handledTaskId = id;
    this.isItUpdate = value;
    this.fileInput.nativeElement.click();
  }


}
