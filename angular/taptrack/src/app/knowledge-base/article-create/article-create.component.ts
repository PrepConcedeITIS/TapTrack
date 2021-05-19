import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {FormGroup} from '@angular/forms';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {Location} from '@angular/common';
import {Router} from '@angular/router';
import {Option} from '../../_interfaces/option';
import {KnowledgeBaseService} from '../knowledge-base.service';

@Component({
  selector: 'app-article-create',
  templateUrl: './article-create.component.html',
  styleUrls: ['./article-create.component.scss']
})
export class ArticleCreateComponent implements OnInit {
  form = new FormGroup({});
  model: CreateArticleCommand = {belongsToId: '', title: '', content: ''};
  fields: FormlyFieldConfig[];

  constructor(private http: HttpClient, private location: Location, private router: Router,
              private knowledgeBaseService: KnowledgeBaseService) {
  }

  ngOnInit(): void {
    this.fields = [
      {
        key: 'belongsToId',
        type: 'select',
        templateOptions: {
          label: 'Choose article project here',
          required: true,
          options: this.http.get<Option[]>(environment.apiUrl + '/articles/options')
        }
      },
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
      .post<string>(environment.apiUrl + '/articles', model)
      .subscribe(id => {
        this.knowledgeBaseService.needToRefreshArticles.next(true);
        this.router.navigate(['/article/details', id]);
      });
  }

  goBack(): void {
    this.location.back();
  }
}

interface CreateArticleCommand {
  belongsToId: string;
  title: string;
  content: string;
}
