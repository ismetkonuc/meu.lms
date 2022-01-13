import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';  
import * as signalR from '@aspnet/signalr';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';  
import { Message } from 'src/app/shared/models/Message';
import { IMessageList } from 'src/app/shared/models/IMessageList';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IUserChat } from 'src/app/shared/models/IUserChat';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  targetUserId = -1;
  targetUser: BehaviorSubject<number>;

  constructor(private http:HttpClient) {
      this.targetUser = new BehaviorSubject(this.targetUserId);
   }

   
  
   getMessages(targetUserId:number) {
    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)

     return this.http.get<IMessageList[]>('https://localhost:44336/api/Chat?targetUserId='+ targetUserId, {headers : headers})
   }

   getLastMessageId(targetUserId:number) {
    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)

     return this.http.get<number>('https://localhost:44336/api/Chat/lastMessage?targetUserId'+ targetUserId, {headers : headers})
   }
  
   getUserChats() {

    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)

    return this.http.get<IUserChat[]>('https://localhost:44336/api/Chat/userList', {headers : headers})
  }

  getTargetUser(id:number){
    this.targetUser.next(id);
  }

  sendMessage(messageModel:any){
    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)

    return this.http.post('https://localhost:44336/api/Chat',messageModel, {headers: headers})
  }

  

}
