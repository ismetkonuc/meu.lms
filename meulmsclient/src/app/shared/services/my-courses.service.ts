import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICourseList } from '../models/ICourseList';
import { ITask } from '../models/ITask';

@Injectable({
  providedIn: 'root'
})
export class MyCoursesService {
  
  taskId = 1;
  task: BehaviorSubject<number>;
  baseUrl = environment.apiUrl;
  
  constructor(private httpClient:HttpClient) { 
    this.task = new BehaviorSubject(this.taskId);
  }

  getMyCourses(){
    let token = localStorage.getItem('token')
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`)

    return this.httpClient.get<ICourseList[]>(this.baseUrl + 'Assignments/allAssignments', {headers});

  }

  getHeader(){
    let token = localStorage.getItem('token')
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`)

    return headers;
  }

  getSpesificTask(id:number){
    this.task.next(id);
  }

}
