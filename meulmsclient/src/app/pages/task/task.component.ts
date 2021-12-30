import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CourseService } from 'src/app/course/course.service';
import { IAddTask } from 'src/app/shared/models/IAddTask';
import { ICourse } from 'src/app/shared/models/ICourse';
import { Status } from 'src/app/shared/models/Status';
import { TaskService } from 'src/app/shared/services/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  courses:ICourse[] = [];
  valueContent: string = '';
  editorValueType: string = '';
  title:string='';
  selected = '';
  addTaskModel:IAddTask = {courseId: 0, detail:this.valueContent, title:'', status: Status.Active, expirationDateAsString: ''};

  constructor(private taskService:TaskService, private courseService:CourseService, private toastr: ToastrService) { 
    this.valueContent = taskService.getMarkup();
  }

  
  ngOnInit(): void {
    this.getCourses();
  }

  printData(){
    this.showSuccess()
    this.addTaskModel.courseId = parseInt(this.selected);
    this.addTaskModel.detail = this.valueContent;
    this.addTaskModel.title = this.title;
    console.log(this.addTaskModel)
    this.taskService.postTask(this.addTaskModel).subscribe();
  }

  getCourses(){
    this.courseService.getCourses().subscribe(response=>{
      this.courses = response;
    });
  }

  showSuccess() {
    this.toastr.success('Ödev Başarıyla Yayınlandı!', 'Yayınlandı...');
  }

}
