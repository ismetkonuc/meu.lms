import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/shared/models/IUser';
import { Observable, of } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  currentUser$ : Observable<IUser> = new Observable<IUser>();
  userRole:any;
  constructor(private accountService : AccountService) {
    this.userRole = localStorage.getItem('role');
   }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  logOut(){
    this.accountService.logout();
  }

}
