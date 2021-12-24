import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { CourseModule } from './course/course.module';
import { HomeModule } from './home/home.module';
import {MatButtonModule} from '@angular/material/button';
import {MatRippleModule} from '@angular/material/core';
import {MatTabsModule} from '@angular/material/tabs';
import {MatBottomSheetModule} from '@angular/material/bottom-sheet';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import { DxTileViewModule, DxButtonModule, DxListModule, DxDataGridModule, DxSelectBoxModule, DxCheckBoxModule, DxHtmlEditorModule, DxButtonGroupModule } from 'devextreme-angular';
import { MyCoursesComponent } from './pages/my-courses/my-courses-main/my-courses.component';
import {MatNativeDateModule} from '@angular/material/core';
import { MaterialModule } from 'src/material.module';
import { TaskAssignmentsComponent } from './pages/my-courses/task-assignments/task-assignments.component';
import { PostArticleComponent } from './pages/post-article/post-article.component';
@NgModule({
  declarations: [
    AppComponent,
    MyCoursesComponent,
    TaskAssignmentsComponent,
    PostArticleComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    CourseModule,
    HomeModule,
    MatButtonModule,
    MatRippleModule,
    MatTabsModule,
    MatBottomSheetModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatCardModule,
    MatInputModule,
    DxTileViewModule,
    DxButtonModule,
    DxListModule,
    DxDataGridModule,
    MatNativeDateModule,
    MaterialModule,
    DxSelectBoxModule,
    DxCheckBoxModule,
    DxHtmlEditorModule,
    DxButtonGroupModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
