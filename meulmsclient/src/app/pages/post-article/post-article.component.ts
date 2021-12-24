import { Component, OnInit } from '@angular/core';
import { ArticleService } from 'src/app/shared/services/article.service';
import 'devextreme/ui/html_editor/converters/markdown';
import { IArticleAddModel } from 'src/app/shared/models/IArticlesAddModel';
import { CourseService } from 'src/app/course/course.service';
import { ICourse } from 'src/app/shared/models/ICourse';
@Component({
  selector: 'app-post-article',
  templateUrl: './post-article.component.html',
  styleUrls: ['./post-article.component.css']
})
export class PostArticleComponent implements OnInit {

  courses:ICourse[] = [];
  valueContent: string = '';
  editorValueType: string = '';
  title:string='';
  selected = '';
  articleModel:IArticleAddModel = {courseId: 0, text:this.valueContent, title:''};
  constructor(private articleService:ArticleService, private courseService:CourseService) {
    this.valueContent = articleService.getMarkup();
   }

  ngOnInit(): void {
    this.getCourses();
  }

  onValueTypeChanged(addedItems:any) {
    this.editorValueType = addedItems[0].text.toLowerCase();
  }

  printData(){

    this.articleModel.courseId = parseInt(this.selected);
    this.articleModel.text = this.valueContent;
    this.articleModel.title = this.title;
    this.articleService.postArticle(this.articleModel).subscribe();

    // console.log(this.valueContent);

  }

  getCourses(){
    this.courseService.getCourses().subscribe(response=>{
      this.courses = response;
    });
  }


}
