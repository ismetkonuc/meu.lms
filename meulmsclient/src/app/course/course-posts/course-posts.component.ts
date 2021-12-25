import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IArticle } from 'src/app/shared/models/IArticle';
import { ArticleService } from 'src/app/shared/services/article.service';

@Component({
  selector: 'app-course-posts',
  templateUrl: './course-posts.component.html',
  styleUrls: ['./course-posts.component.css']
})
export class CoursePostsComponent implements OnInit {
  
  @Input() courseId:any;
  activatedRouteId:any;
  courseArticles: IArticle[] = [];

  constructor(private articleService: ArticleService, private activatedRoute: ActivatedRoute) { 
    this.activatedRouteId = Number(this.activatedRoute.snapshot.paramMap.get('id'))
    this.getPosts();
  }

  ngOnInit(): void {
    
  }

  getPosts() {
    this.articleService.getPostsByCourseId(this.activatedRouteId).subscribe(response => {
      this.courseArticles = response; 
      console.log(this.courseArticles)
    })
  }

  insertHTML(text:any, id:any){

    console.log("clicked")

    let element = document.getElementById('post-body-'+ id);

    element!.innerHTML = text;


    // element?.insertAdjacentHTML('beforebegin', text);

  }

}
