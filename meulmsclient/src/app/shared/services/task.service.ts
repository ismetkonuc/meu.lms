import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

// const markup = ''

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  markup = '';

  constructor(private http: HttpClient) { }

  getMarkup(): string {
    return this.markup;
  }

  postTask(taskModel:any){


    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)
    return this.http.post('https://localhost:44336/api/tasks', taskModel,{headers : headers})
    // return this.http.post('https://localhost:44336/api/Articles', articleModel)
  }

  updateTask(task:any){

    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)


    return this.http.put('https://localhost:44336/api/tasks/', task, {headers:headers});

  }

  getTaskWithId(taskId:any){
    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)
    return this.http.get<Task>('https://localhost:44336/api/Tasks/single/'+ Number(taskId), {headers:headers});
  }

  deleteTask(id:any){
    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)
    return this.http.delete('https://localhost:44336/api/Tasks/'+ id,{headers:headers});
  }

}
