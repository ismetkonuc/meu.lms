import { Component, NgZone, OnInit } from '@angular/core';
import { Message } from 'src/app/shared/models/Message';
import { ChatService } from './chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  title = 'ClientApp';  
  txtMessage: string = '';  
  uniqueID: string = new Date().getTime().toString();  
  messages = new Array<Message>();  
  message = new Message();  

  constructor(private chatService: ChatService,  private _ngZone: NgZone) { 
    this.subscribeToEvents();  
  }

  ngOnInit(): void {
  }

  sendMessage(): void {  
    if (this.txtMessage) {  
      this.message = new Message();  
      this.message.clientuniqueid = this.uniqueID;  
      this.message.type = "sent";  
      this.message.message = this.txtMessage;  
      this.message.date = String(new Date());  
      this.messages.push(this.message);  
      this.chatService.sendMessage(this.message);  
      this.txtMessage = ''; 
    }  
  } 

  private subscribeToEvents(): void {  
  
    this.chatService.messageReceived.subscribe((message: Message) => {  
      this._ngZone.run(() => {  
        if (message.clientuniqueid !== this.uniqueID) {  
          message.type = "received";  
          this.messages.push(message);  
        }  
      });  
    });  
  } 

}
