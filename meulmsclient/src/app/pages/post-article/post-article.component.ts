import { Component, OnInit } from '@angular/core';
import { ArticleService } from 'src/app/shared/services/article.service';
import 'devextreme/ui/html_editor/converters/markdown';
@Component({
  selector: 'app-post-article',
  templateUrl: './post-article.component.html',
  styleUrls: ['./post-article.component.css']
})
export class PostArticleComponent implements OnInit {

  valueContent: string = '';
  editorValueType: string = '';

  constructor(private articleService:ArticleService) {
    this.valueContent = articleService.getMarkup();
   }

  ngOnInit(): void {
  }

  onValueTypeChanged(addedItems:any) {
    this.editorValueType = addedItems[0].text.toLowerCase();
  }

  printData(data:any){

    document.getElementById("articleArea").insertAdjacentHTML("afterbegin", this.valueContent);

    console.log(this.valueContent);

  }


}
