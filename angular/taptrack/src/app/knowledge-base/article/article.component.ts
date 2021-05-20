import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {KnowledgeBaseService} from '../knowledge-base.service';
import {delay} from 'rxjs/operators';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {
  opened = true;
  selectedArticleId: string;
  projectsWithArticles: Observable<ProjectWithArticles[]>;
  projects: ProjectWithArticles[];

  constructor(private http: HttpClient, private knowledgeBaseService: KnowledgeBaseService) {
  }

  ngOnInit(): void {
    this.getData();

    this.knowledgeBaseService.needToRefreshArticles.subscribe(next => {
      if (next === true) {
        this.getData();
      }
    });
  }

  private getData() {
    this.http.get<ProjectWithArticles[]>(environment.apiUrl + '/articles')
      .subscribe(x => {
        this.projects = x;
      });
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
