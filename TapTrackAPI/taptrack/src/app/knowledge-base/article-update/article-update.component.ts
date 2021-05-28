import {Component, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {HttpClient} from "@angular/common/http";
import {Location} from "@angular/common";
import {Router} from "@angular/router";
import {environment} from "../../../environments/environment";
import {FullArticle} from "../_interfaces/full-article";

@Component({
  selector: 'app-article-update',
  templateUrl: './article-update.component.html',
  styleUrls: ['./article-update.component.scss']
})
export class ArticleUpdateComponent implements OnInit {
  article: FullArticle;
  form = new FormGroup({});
  model: UpdateArticleCommand = {id: '', belongsToId: '', title: '', content: ''};
  fields: FormlyFieldConfig[];

  constructor(private http: HttpClient, private location: Location, private router: Router) {
    this.article = this.router.getCurrentNavigation().extras.state as FullArticle;
  }

  ngOnInit(): void {
    this.model.id = this.article.id;
    this.model.belongsToId = this.article.belongsToId;
    this.model.title = this.article.title;
    this.model.content = this.article.content;
    this.fields = [
      {
        key: 'title',
        type: 'input',
        templateOptions: {
          label: 'Title',
          placeholder: 'Type or paste article title here',
          required: true
        }
      },
      {
        key: 'content',
        type: 'md-editor'
      }
    ];
  }

  onSubmit(model) {
    this.http
      .put<string>(environment.apiUrl + '/articles', model)
      .subscribe(id => this.router.navigate(['/article/details', id]));
  }

  goBack(): void {
    this.location.back();
  }
}

interface UpdateArticleCommand {
  id: string;
  belongsToId: string;
  title: string;
  content: string;
}
