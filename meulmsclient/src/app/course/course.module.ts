import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CourseComponent } from './course.component';
import { CourseItemComponent } from './course-item/course-item.component';
import { CourseDetailsComponent } from './course-details/course-details.component';
import { RouterModule } from '@angular/router';
import {MatButtonModule} from '@angular/material/button'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatRippleModule} from '@angular/material/core';
import {MatTabsModule} from '@angular/material/tabs';
import {MatBottomSheetModule} from '@angular/material/bottom-sheet';
import {MatCardModule} from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatExpansionModule} from '@angular/material/expansion';
import { CoursePostsComponent } from './course-posts/course-posts.component';

@NgModule({
  declarations: [
    CourseComponent,
    CourseItemComponent,
    CourseDetailsComponent,
    CoursePostsComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatButtonModule,
    BrowserAnimationsModule,
    MatRippleModule,
    MatTabsModule,
    MatBottomSheetModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule
  ],
  exports: [CourseComponent]
})
export class CourseModule { }
