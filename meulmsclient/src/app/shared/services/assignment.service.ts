import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AssignmentService {

  constructor(private http:HttpClient) { }

  deleteAssignment(assignmentId:any){

    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)
    return this.http.delete('https://localhost:44336/api/Assignments/'+ assignmentId,{headers:headers});
  }

}
