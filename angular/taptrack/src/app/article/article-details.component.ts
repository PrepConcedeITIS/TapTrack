import {Component, Inject, Input, OnInit} from '@angular/core';
import {Article, ArticleService} from "../_services/article.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-article',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.scss']
})
export class ArticleDetailsComponent implements OnInit {
  @Input() selectedArticleId: string;
  article: Observable<Article>;

  constructor(private articleService: ArticleService) {
  }

  ngOnInit(): void {
    this.article = this.articleService.getArticle(this.selectedArticleId);
  }
}
