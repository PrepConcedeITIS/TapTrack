import {Component, Input, OnChanges, SimpleChanges} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {FormGroup} from '@angular/forms';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {PageChangedEvent} from 'ngx-bootstrap/pagination';
import {Comment} from '../_interfaces/comment';
import {DateBeautifierService} from '../../_services/date-beautifier.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnChanges {
  @Input() entityType: 'Issue' | 'Article';
  @Input() entityId: string;
  @Input() projectId: string;
  form = new FormGroup({});
  model: CreateCommentCommand = {entityType: '', entityId: '', projectId: '', text: ''};
  fields: FormlyFieldConfig[];
  comments: Comment[];
  returnedComments: Comment[];

  dateBeautifier: DateBeautifierService;

  constructor(private http: HttpClient, dateBeautifier: DateBeautifierService) {
    this.dateBeautifier = dateBeautifier;
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.comments = undefined;
    this.model.entityType = this.entityType;
    this.model.entityId = this.entityId;
    this.model.projectId = this.projectId;
    this.fields = [
      {
        key: 'text',
        type: 'md-editor'
      }
    ];
    this.http
      .get<Comment[]>(
        environment.apiUrl + '/comments',
        {params: {entityType: this.entityType, entityId: this.entityId}})
      .subscribe(response => this.comments = response);
  }

  onSubmit(model) {
    console.log(model);
    this.http
      .post<Comment>(environment.apiUrl + '/comments', model)
      .subscribe(response => {
        this.form.reset();
        this.comments.unshift(response);
      });
  }

  goToEdit(comment: Comment): void {
    comment.mode = 'editor';
  }

  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.returnedComments = this.comments.slice(startItem, endItem);
  }
}

interface CreateCommentCommand {
  entityType: string;
  entityId: string;
  projectId: string;
  text: string;
}
