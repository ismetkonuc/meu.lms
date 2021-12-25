import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';  
import * as signalR from '@aspnet/signalr';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';  
import { Message } from 'src/app/shared/models/Message';
@Injectable({
  providedIn: 'root'
})
export class ChatService {

  messageReceived = new EventEmitter<Message>();  
  connectionEstablished = new EventEmitter<Boolean>();  

  private connectionIsEstablished = false;  
  private _hubConnection: any;  

  constructor() {
    this.createConnection();  
    this.registerOnServerEvents();  
    this.startConnection();
   }

   sendMessage(message: Message) {  
    
    this._hubConnection.invoke('NewMessage', message).then((data:any)=>console.log(data));  
  }  
  
  private createConnection() {  
    this._hubConnection = new signalR.HubConnectionBuilder()  
      .withUrl("https://localhost:44336/messagehub")  
      .build();  
  } 

  private startConnection(): void {  
    this._hubConnection  
      .start()  
      .then(() => {  
        this.connectionIsEstablished = true;  
        console.log('Hub connection started');  
        this.connectionEstablished.emit(true);  
      })  
      .catch( (err:any) => {  
        console.log('Error while establishing connection, retrying...');  
        setTimeout(function (this: ChatService) { this.startConnection(); }, 5000);  
      });  
  }  

  private registerOnServerEvents(): void {  
    this._hubConnection.on('MessageReceived', (data: any) => {  
      this.messageReceived.emit(data);  
      console.log(data)
    });  
  }  

}
