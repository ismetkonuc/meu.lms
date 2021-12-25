import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IArticle } from '../models/IArticle';
import { IArticleAddModel } from '../models/IArticlesAddModel';

const markup = '';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private http: HttpClient) { 
  }

  getMarkup(): string {
    return markup;
  }

  getPostsByCourseId(courseId:number){

    console.log(courseId);
    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)


    // return this.http.get<IArticle[]>('http://localhost:5000/api/Articles/'+ courseId, {headers:headers})
    return this.http.get<IArticle[]>('https://localhost:44336/api/Articles/'+ courseId, {headers:headers} )
  
  }

  postArticle(articleModel:any){

    // console.log(articleModel);

    let currentUserToken = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${currentUserToken}`)
    
    return this.http.post('http://localhost:5000/api/articles', articleModel,{headers : headers})
    // return this.http.post('https://localhost:44336/api/Articles', articleModel)
  }
  
}
