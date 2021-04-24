import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {FullArticle} from "../_interfaces/full-article";

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.scss']
})
export class ArticleDetailsComponent implements OnInit {
  article: FullArticle;
  command: DeleteArticleCommand = {id: '', belongsToId: ''};

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.http
        .get<FullArticle>(environment.apiUrl + `/articles/${params.id}`)
        .subscribe(x => this.article = x);
    });
  }

  goToEdit(id: string): void {
    this.router
      .navigate(['article/edit', id], {state: this.article})
      .then();
  }

  deleteArticle() {
    this.command.id = this.article.id;
    this.command.belongsToId = this.article.belongsToId;
    console.log(this.command);
    this.http
      .request('delete', environment.apiUrl + '/articles', {body: this.command})
      .subscribe(() => {
        this.router
          .navigate(['article'])
          .then();
      });
  }
}

interface DeleteArticleCommand {
  id: string;
  belongsToId: string;
}
