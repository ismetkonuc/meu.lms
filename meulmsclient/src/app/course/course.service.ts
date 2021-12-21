import { HttpClient, HttpHeaders } from '@angular/common/http';
import { applySourceSpanToExpressionIfNeeded } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { IAssignment } from '../shared/models/IAssignment';
import { ICourse } from '../shared/models/ICourse';
import { ITask } from '../shared/models/ITask';
@Injectable({
  providedIn: 'root'
})

export class CourseService {

  headers = new HttpHeaders();

  constructor(private http:HttpClient) { 
    
  }

  baseUrl = "http://localhost:5000/api/"
  course:ICourse = {id: 0, name : '', code:''};
  
  getCourses(){
    let currentUserToken = localStorage.getItem('token');
    let headers = this.headers.set('Authorization', `Bearer ${currentUserToken}`)
    
    return this.http.get<ICourse[]>(this.baseUrl + 'courses', {headers})
  }

  getTasks(courseId:number){

    let currentUserToken = localStorage.getItem('token');
    let headers = this.headers.set('Authorization', `Bearer ${currentUserToken}`)
    
    return this.http.get<ITask[]>(this.baseUrl + 'tasks/' + courseId, {headers})
  }

  async getCourseName(courseId:number): Promise<any>{
    const courses = await this.getCourses().toPromise();
    return courses.find(({ name: n, id: i }) => i === courseId)?.name;
  }

  getUserAssignments(courseId:number){
    // let currentUserToken = localStorage.getItem('token');

    // let headers = new HttpHeaders();
    // headers = headers.set('Authorization', `Bearer ${currentUserToken}`)

    // return this.http.get<IAssignment[]>(this.baseUrl+ 'Assignments/sendedAssignments?courseId='+ courseId, {headers})
    // return this.http.get<IAssignment[]>(this.baseUrl+ 'Assignments/sendedAssignments?courseId='+ courseId, {headers})
  }

}
