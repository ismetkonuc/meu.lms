import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/models/IUser';
import { map } from 'rxjs/operators'
import { ITask } from '../shared/models/ITask';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<IUser>({ email: '', displayName: '', token: '' });
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private httpClient: HttpClient, private router: Router) { }

  getCurrentUserValue(){
    return this.currentUserSource.value;
  }

  loadCurrentUser(token:string){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`)

    return this.httpClient.get(this.baseUrl + 'auth/currentuser', {headers}).pipe(
      map((user:any)=>{
        if(user){
          localStorage.setItem('token', user.token);
          localStorage.setItem('role', user.role)
          this.currentUserSource.next(user)
        }
      })
    )
  }


  login(values: any) {
    return this.httpClient.post(this.baseUrl + 'auth/login', values).pipe(
      map((user: any) => {
      if (user) {
        localStorage.setItem('token', user.token);
        localStorage.setItem('role', user.role);

        this.currentUserSource.next(user);
        location.href="/"

      }
    })
    );
  }

  register(values: any) {
    return this.httpClient.post(this.baseUrl + 'auth/register', values).pipe(
      map((user: any) => {
      if (user) {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      }
    })
    );
  }


  logout() {
    
    localStorage.removeItem('token');
    localStorage.removeItem('role');

    this.currentUserSource.next({ email: '', displayName: '', token: '' });
    this.router.navigateByUrl('/account/login');

  }

}
