import { Component, Input, OnInit } from '@angular/core';
import { Message } from 'src/app/shared/models/Message';
import { ChatService } from '../chat.service';

@Component({
  selector: 'app-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.css']
})
export class ChatBoxComponent implements OnInit {

  id:number=1;
  targetUser:any;
  messages: Message[] = [];
  content:string='';
  inputVal = '';
  previousValue = 0;
  currentValue = 0;

  constructor(private chatService:ChatService) { 
  }

  ngOnInit(): void {

    setTimeout(() => { this.ngOnInit() }, 1000 * 10)


    this.chatService.targetUser.subscribe(async (targetUser)=>{
      this.id = targetUser;
      this.getMessages();

      this.chatService.getLastMessageId(this.id).subscribe((lastValue)=>{
          this.currentValue = lastValue;

          if(this.currentValue != this.previousValue){
            this.previousValue = this.currentValue;
            this.getMessages();
          }

      });


    })

  }

  getMessages(){
    this.chatService.getMessages(this.id).subscribe( (response:any)=>{
      this.messages = response;
    })
  }

  sendMessage(){
    const sendMessage = {"Content": "", "MessageTo": this.id}
    sendMessage.Content = this.content;

    this.chatService.sendMessage(sendMessage).subscribe(()=>{
      this.ngOnInit();
    });

    // console.log(sendMessage)
  }

  play(){
    var audio = new Audio('./assets/sound/beep.ogg');
    audio.play();
  }

}
