import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CourseService } from 'src/app/course/course.service';
import { IAddTask } from 'src/app/shared/models/IAddTask';
import { ICourse } from 'src/app/shared/models/ICourse';
import { Status } from 'src/app/shared/models/Status';
import { TaskService } from 'src/app/shared/services/task.service';
import 'moment/locale/tr';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css'],
})
export class TaskComponent implements OnInit {

  courses:ICourse[] = [];
  valueContent: string = '';
  editorValueType: string = '';
  title:string='';
  selected = '';
  selectedDate :any;
  addTaskModel:IAddTask = {courseId: 0, detail:this.valueContent, title:'', status: Status.Active, expirationDateAsString: ''};
  events: string[] = [];
  constructor(private taskService:TaskService, private courseService:CourseService, private toastr: ToastrService) { 
    this.valueContent = taskService.getMarkup();
  }

  
  ngOnInit(): void {
    this.getCourses();
  }

  addEvent(event: MatDatepickerInputEvent<Date>) {
    this.selectedDate = event.value?.toLocaleDateString();
  }

  printData(){
    this.showSuccess()
    this.addTaskModel.courseId = parseInt(this.selected);
    this.addTaskModel.detail = this.valueContent;
    this.addTaskModel.title = this.title;
    this.addTaskModel.expirationDateAsString = this.selectedDate.toString();
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
