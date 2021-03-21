import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {User} from "./authentication.service";

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  constructor(private http: HttpClient) {
  }

  public getArticle(id: string): Observable<Article> {
    return this.http.get<Article>(environment.apiUrl + `/article/${id}`);
  }
}

export interface Article {
  id: string;
  title: string;
  createdBy: User;
  createdAt: Date;
  updatedBy: User;
  updatedAt: Date;
  content: string;
}
