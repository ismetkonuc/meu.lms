import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IArticle } from 'src/app/shared/models/IArticle';
import { IAssignment } from 'src/app/shared/models/IAssignment';
import { ITask } from 'src/app/shared/models/ITask';
import { ArticleService } from 'src/app/shared/services/article.service';
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
  courseArticles: IArticle[] = [];
  userAssignments: IAssignment[] = [];
  activatedRouteId: number;
  courseName: string = '';
  isItUpdate: boolean = false;
  handledTaskId: number = 0;
  counter = 0;
  panelOpenState = false;
  constructor(private courseService: CourseService, private activatedRoute: ActivatedRoute, private http: HttpClient, private articleService: ArticleService) {
    
    this.activatedRouteId = Number(this.activatedRoute.snapshot.paramMap.get('id'))
  
  }


  ngOnInit(): void {
    this.loadCourse();
    this.getPosts();
  }


  getPosts() {
    this.articleService.getPostsByCourseId(this.activatedRouteId).subscribe(response => {
      this.courseArticles = response; 
    })
  }


  async loadCourse() {

    this.courseName = await this.courseService.getCourseName(this.activatedRouteId)

    this.courseService.getTasks(this.activatedRouteId).subscribe(courseTasks => {
      this.courseTasks = courseTasks;
    },
      error => {
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

    if (this.isItUpdate) {
      return this.http.put('http://localhost:5000/api/assignments/', formData, { headers: headers })
        .subscribe(() => this.ngOnInit());
    }


    return this.http.post('http://localhost:5000/api/assignments/', formData, { headers: headers })
      .subscribe(() => this.ngOnInit());

  }



  onClickFileInputButton(value: any, id: any): void {
    this.handledTaskId = id;
    this.isItUpdate = value;
    this.fileInput.nativeElement.click();
  }


}
