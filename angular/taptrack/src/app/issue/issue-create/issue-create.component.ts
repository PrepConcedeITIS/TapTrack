import { Component, OnInit } from '@angular/core';
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {tap} from "rxjs/operators";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {IssueCreateDto} from "../IssueCreateDto";
import {environment} from "../../../environments/environment";
import {Project} from "../../project/_interfaces/project";

@Component({
  selector: 'app-issue-create',
  templateUrl: './issue-create.component.html',
  styleUrls: ['./issue-create.component.scss']
})
export class IssueCreateComponent implements OnInit {

  constructor(private router: Router,
              private route: ActivatedRoute,
              private httpClient: HttpClient) {
  }

  serverValidationErrors: string;
  form = new FormGroup({});
  model: IssueCreateDto = {description: '', name: ''};
  fields: FormlyFieldConfig[] = [
    {
      key: 'project',
      type: 'select',
      templateOptions: {
        label: 'Project',
        required: true,
        hideRequiredMarker: true,
        valueProp: 'id',
        labelProp: 'name',
        options: this.getProjectsList()
      }
    },
    {
      key: 'name',
      type: 'input',
      className: '',
      templateOptions: {
        label: 'Issue Name',
        placeholder: 'Enter your new issue name',
        required: true,
        maxLength: 100,
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
        rows: 10
      }
    },
  ];

  ngOnInit(): void {

  }

  onSubmit() {
    this.createNewIssue()
      .pipe(tap((issueId) => {
          this.router.navigate([`issue/${issueId}`]);
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

  createNewIssue(): Observable<string> {
    const formData = new FormData();
    const keys = Object.getOwnPropertyNames(this.model);
    keys.forEach((propertyName) => {
      formData.append(propertyName, this.model[propertyName]);
    });

    return this.httpClient.post<string>(`${environment.apiUrl}/issue/create/${this.model['project']}`, formData);
  }

  getProjectsList(): Observable<any[]> {
    return this.httpClient.get<any[]>(`${environment.apiUrl}/project/get`);
  }
}
