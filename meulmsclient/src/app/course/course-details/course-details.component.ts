import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IArticle } from 'src/app/shared/models/IArticle';
import { IAssignment } from 'src/app/shared/models/IAssignment';
import { ITask } from 'src/app/shared/models/ITask';
import { ArticleService } from 'src/app/shared/services/article.service';
import { AssignmentService } from 'src/app/shared/services/assignment.service';
import { TaskService } from 'src/app/shared/services/task.service';
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
  userRole : any;
  constructor(private taskService: TaskService, private courseService: CourseService, private activatedRoute: ActivatedRoute, 
    private http: HttpClient, private articleService: ArticleService, private assignmentService: AssignmentService) {

    this.activatedRouteId = Number(this.activatedRoute.snapshot.paramMap.get('id'))
    this.userRole = localStorage.getItem('role');
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

  isExpired(date:any){
    var dateNow = new Date();
    var sendedDate = new Date(date);
    console.log(dateNow>sendedDate);
    return dateNow>sendedDate;
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

  deleteTask(taskId: any) {
    var res = window.confirm("Bu ??dev silinecek emin misiniz?");
    if (res) {
      this.taskService.deleteTask(taskId).subscribe(res => {
        console.log(res)
        this.ngOnInit();
      });
    }
  }

  deleteAssignment(assignmentId:any){

    var res = window.confirm("Bu g??nderim silinecek emin misiniz?");

    if(res){
      this.assignmentService.deleteAssignment(assignmentId).subscribe(res => {
        this.ngOnInit();
      });
    }
  }

  insertHTML(text:any, id:any){

    var cleanText = text.replace(/<\/?[^>]+(>|$)/g, "");
    return cleanText;

  }


}
