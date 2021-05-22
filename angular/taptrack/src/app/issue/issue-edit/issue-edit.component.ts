import { Component, OnInit } from '@angular/core';
import {FormGroup} from "@angular/forms";
import {IssueCreateDto} from "../IssueCreateDto";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {IssueEditDto} from "./IssueEditDto";
import {tap} from "rxjs/operators";

@Component({
  selector: 'app-issue-edit',
  templateUrl: './issue-edit.component.html',
  styleUrls: ['./issue-edit.component.scss']
})
export class IssueEditComponent implements OnInit {

  constructor(private router: Router,
              private route: ActivatedRoute,
              private httpClient: HttpClient) { }
  private issueId: string;
  serverValidationErrors: string;
  form = new FormGroup({});
  model: IssueEditDto = {description: '', name: '', projectId: ''};
  fields: FormlyFieldConfig[] = [
    {
      key: 'projectId',
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
      key: 'title',
      type: 'input',
      className: '',
      templateOptions: {
        label: 'Issue Name',
        placeholder: 'Enter your issue name',
        required: true,
        maxLength: 500,
        hideRequiredMarker: true
      }
    },
    {
      key: 'description',
      type: 'textarea',
      templateOptions: {
        label: 'Issue Description',
        placeholder: 'Your issue description',
        required: false,
        rows: 10
      }
    },
  ];

  getProjectsList(): Observable<any[]> {
    return this.httpClient.get<any[]>(`${environment.apiUrl}/project/get`);
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.issueId = params.id;
    });
    this.httpClient.get<IssueEditDto>(`${environment.apiUrl}/issue/edit/${this.issueId}`)
      .subscribe(x => {
        this.model = x;
      });
  }

  onSubmit() {
    this.editIssue()
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

  editIssue(): Observable<string> {
    const formData = new FormData();
    const keys = Object.getOwnPropertyNames(this.model);
    keys.forEach((propertyName) => {
      formData.append(propertyName, this.model[propertyName]);
    });

    return this.httpClient.post<string>(`${environment.apiUrl}/issue/edit/${this.issueId}`, formData);
  }
}
