import { Component, NgZone, OnInit } from '@angular/core';
import { IMessageList } from 'src/app/shared/models/IMessageList';
import { IUserChat } from 'src/app/shared/models/IUserChat';
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
  messages: Message[] = [];
  chatbox: IUserChat[] = [];
  searchString = ''


  constructor(private chatService:ChatService) { 
    this.getUserChat();
  }

  ngOnInit(): void {
  }


  getUserChat(){
    this.chatService.getUserChats().subscribe((response:any)=>{
      this.chatbox = response;
      console.log(this.chatbox)
    })
  }

  getTargetUser(id:number){
    this.chatService.getTargetUser(id);
  }
 

}
