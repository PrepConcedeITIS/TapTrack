import {Component, OnInit} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {
  projectsWithArticles: Observable<ProjectWithArticles[]>;

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.projectsWithArticles = this.http.get<ProjectWithArticles[]>(environment.apiUrl + '/article');
  }
}

interface ProjectWithArticles {
  name: string;
  articles: ShortArticle[];
}

interface ShortArticle {
  id: string;
  title: string;
}
