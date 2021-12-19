import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { CourseModule } from '../course/course.module';



@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    CourseModule
  ],
  exports: [HomeComponent]
})
export class HomeModule { }
