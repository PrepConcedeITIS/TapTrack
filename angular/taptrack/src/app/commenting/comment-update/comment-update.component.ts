import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {Comment} from "../_interfaces/comment";

@Component({
  selector: 'app-comment-update',
  templateUrl: './comment-update.component.html',
  styleUrls: ['./comment-update.component.scss']
})
export class CommentUpdateComponent implements OnInit {
  @Input() comment: Comment;
  form = new FormGroup({});
  model: UpdateCommentCommand = {id: '', text: ''};
  fields: FormlyFieldConfig[];

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.model.id = this.comment.id;
    this.model.text = this.comment.text;
    this.fields = [
      {
        key: 'text',
        type: 'md-editor'
      }
    ];
  }

  onSubmit(model) {
    this.http
      .put<EditedCommentDTO>(environment.apiUrl + '/comments', model)
      .subscribe(response => {
        this.comment.mode = 'preview';
        this.comment.text = response.text;
        this.comment.lastUpdated = response.lastUpdated;
      });
  }

  goBack(): void {
    this.comment.mode = 'preview';
  }
}

interface UpdateCommentCommand {
  id: string;
  text: string;
}

interface EditedCommentDTO {
  text: string;
  lastUpdated: Date;
}
