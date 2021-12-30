import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CourseService } from 'src/app/course/course.service';
import { ICourse } from 'src/app/shared/models/ICourse';
import { TaskService } from 'src/app/shared/services/task.service';

@Component({
  selector: 'app-update-task',
  templateUrl: './update-task.component.html',
  styleUrls: ['./update-task.component.css']
})
export class UpdateTaskComponent implements OnInit {

  activatedRouteId = 0;
  task: any;
  courses:ICourse[] = [];
  valueContent: string = '';
  editorValueType: string = '';
  title:string='';
  selected = '';
  constructor(private activatedRoute: ActivatedRoute, private taskService: TaskService, private courseService: CourseService, private toastr: ToastrService) {
    this.activatedRouteId = Number(this.activatedRoute.snapshot.paramMap.get('id'))
  }

  ngOnInit(): void {
    this.getCourses();
    this.getTaskWithId(this.activatedRouteId);
  }

  getTaskWithId(id: any) {
    this.taskService.getTaskWithId(id).subscribe(res => {
      this.task = res;
      this.valueContent = this.task.detail;
      this.title = this.task.title;
    });
  }

  getCourses(){
    this.courseService.getCourses().subscribe(response=>{
      this.courses = response;
    });
  }

  showSuccess() {
    this.toastr.success('Ödev Başarıyla Güncellendi!', 'Güncellendi...');
  }

  updateTask(){
    this.showSuccess();
    this.task.detail = this.valueContent;
    this.task.title = this.title;
    this.task.courseId = Number(this.selected);
    this.taskService.updateTask(this.task).subscribe(res=>{
    });
  }

}
