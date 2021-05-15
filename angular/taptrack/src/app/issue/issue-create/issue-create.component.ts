import { Component, OnInit } from '@angular/core';
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {tap} from "rxjs/operators";
import {HttpErrorResponse} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {Router} from "@angular/router";
import {IssueCreateDto} from "./IssueCreateDto";

@Component({
  selector: 'app-issue-create',
  templateUrl: './issue-create.component.html',
  styleUrls: ['./issue-create.component.scss']
})
export class IssueCreateComponent implements OnInit {

  constructor(private router: Router) {
  }

  serverValidationErrors: string;
  form = new FormGroup({});
  model: IssueCreateDto = {description: '', name: '', type: '', assignee: '', priority: ''};
  fields: FormlyFieldConfig[] = [
    {
      key: 'name',
      type: 'input',
      className: '',
      templateOptions: {
        label: 'Issue Name',
        placeholder: 'Enter your new issue name',
        required: true,
        maxLength: 30,
        hideRequiredMarker: true
      }
    },
    {
      key: 'description',
      type: 'textarea',
      templateOptions: {
        label: 'Issue Description',
        placeholder: 'Your new issue description',
        required: false,
        maxLength: 500
      }
    },
    {
      key: 'assignee',
      type: 'select',
      templateOptions: {
        label: 'Assignee',
        placeholder: 'Issue assignee',
        required: false,
      }
    },
    {
      key: 'type',
      type: 'select',
      templateOptions: {
        label: 'Type',
        placeholder: 'Issue type',
        required: false,
      }
    },
    {
      key: 'priority',
      type: 'select',
      templateOptions: {
        label: 'Priority',
        placeholder: 'Issue priority',
        required: false,
      }
    },
  ];

  ngOnInit(): void {
  }

  onSubmit() {
    this.createNewIssue()
      .pipe(tap((issue) => {
          this.router.navigate([`issue/details/${issue.id}`]);
        },
        (err: HttpErrorResponse) => {
          switch (err.status) {
            case 422: {
              this.serverValidationErrors = err.error;
            }
          }
        }))
      .subscribe();
  }

  createNewIssue(): Observable<any> {
    return of();
  }
}
