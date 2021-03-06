import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CourseDetailsComponent } from './course/course-details/course-details.component';
import { CourseComponent } from './course/course.component';
import { HomeComponent } from './home/home.component';
import { ChatComponent } from './pages/chat/chat.component';
import { MyCoursesComponent } from './pages/my-courses/my-courses-main/my-courses.component';
import { PostArticleComponent } from './pages/post-article/post-article.component';
import { TaskComponent } from './pages/task/task.component';
import { UpdateTaskComponent } from './pages/task/update-task/update-task.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'course',component:CourseComponent},
  {path:'postarticle',component:PostArticleComponent},
  {path:'chat',component:ChatComponent},
  {path:'task',component:TaskComponent},
  {path:'task/:id', component:UpdateTaskComponent},
  {path:'course/:id', component:CourseDetailsComponent},
  {path:'myCourses',component:MyCoursesComponent},
  {path: 'account', loadChildren: ()=> import('./account/account.module').then(mod=>mod.AccountModule)},
  {path:'**', redirectTo:'', pathMatch: 'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
