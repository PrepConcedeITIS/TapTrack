import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {KnowledgeBaseService} from '../knowledge-base.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {
  opened = true;
  selectedArticleId: string;
  projects: ProjectWithArticles[];
  private projectsSource: ReadonlyArray<ProjectWithArticles>;

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
        this.projectsSource = x;
        this.projects = this.projectsSource.slice();
      });
  }

  filterArticles($event: InputEvent) {
    const query = ($event.target as HTMLInputElement).value;
    this.projects = this.projectsSource.slice().map(value => {
      const filteredArticles = value.articles.filter(article => article.title.toLowerCase().includes(query.toLowerCase()));
      const projectWithArticles: ProjectWithArticles = {articles: filteredArticles, name: value.name};
      return projectWithArticles;
    }).filter(value => value.articles.length !== 0);
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
