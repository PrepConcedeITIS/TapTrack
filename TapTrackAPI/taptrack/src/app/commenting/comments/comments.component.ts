import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {FormGroup} from '@angular/forms';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {Comment} from '../_interfaces/comment';
import {DateService} from '../../_services/date.service';
import {AppSettings} from "../../app-settings";

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit {
  @Input() entityType: 'Issue' | 'Article';
  @Input() entityId: string;
  @Input() projectId: string;
  form = new FormGroup({});
  model: CreateCommentCommand = {entityType: '', entityId: '', projectId: '', text: ''};
  fields: FormlyFieldConfig[] = [
    {
      key: 'text',
      type: 'md-editor'
    }
  ];
  timeShift = AppSettings.ServerTimeShiftHours;
  p = 1;
  comments: Comment[];
  deletedCommentsFlags: Map<string, boolean> = new Map<string, boolean>();
  dateBeautifier: DateService;

  constructor(private http: HttpClient, dateBeautifier: DateService) {
    this.dateBeautifier = dateBeautifier;
  }


  ngOnInit(): void {
    this.comments = undefined;
    this.model.entityType = this.entityType;
    this.model.entityId = this.entityId;
    this.model.projectId = this.projectId;

    this.http
      .get<Comment[]>(
        environment.apiUrl + '/comments',
        {params: {entityType: this.entityType, projectId: this.projectId, entityId: this.entityId}})
      .subscribe(response => {
        this.comments = response;
        this.comments
          .filter(comment => comment.isDeleted)
          .forEach(comment => this.deletedCommentsFlags.set(comment.id, false));
      });
  }

  onSubmit(model) {
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

  showDeletedContent(id: string) {
    this.deletedCommentsFlags.set(id, !this.deletedCommentsFlags.get(id));
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
