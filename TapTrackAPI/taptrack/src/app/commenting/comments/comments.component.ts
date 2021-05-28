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
  p = 1;
  comments: Comment[];

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
        {params: {entityType: this.entityType, projectId: this.projectId, entityId: this.entityId}})
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

  delete(comment: Comment): void {
    const command: DeleteCommentCommand = {
      id: comment.id,
      isCommentBeingDeletedPermanently: false,
      projectId: this.projectId
    };
    this.http
      .request('delete', environment.apiUrl + '/comments', {body: command})
      .subscribe(() => {
        const index = this.comments.findIndex(x => x.id === comment.id);
        this.comments.splice(index, 1);
      });
  }

  restore(comment: Comment): void {
    const command: RestoreCommentCommand = {
      id: comment.id,
      projectId: this.projectId
    };
    this.http
      .post(environment.apiUrl + '/comments/restore/', command)
      .subscribe(() => {
        comment.isDeleted = false;
      });
  }

  deletePermanently(comment: Comment): void {
    const command: DeleteCommentCommand = {
      id: comment.id,
      isCommentBeingDeletedPermanently: true,
      projectId: this.projectId
    };
    this.http
      .request('delete', environment.apiUrl + '/comments', {body: command})
      .subscribe(() => {
        const index = this.comments.findIndex(x => x.id === comment.id);
        this.comments.splice(index, 1);
      });
  }
}

interface CreateCommentCommand {
  entityType: string;
  entityId: string;
  projectId: string;
  text: string;
}

interface DeleteCommentCommand {
  id: string;
  isCommentBeingDeletedPermanently: boolean;
  projectId: string;
}

interface RestoreCommentCommand {
  id: string;
  projectId: string;
}
