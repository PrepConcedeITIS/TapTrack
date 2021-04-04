import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {IssueDto} from "../issueDto";

@Component({
  selector: 'app-issue-list',
  templateUrl: './issue-list.component.html',
  styleUrls: ['./issue-list.component.scss']
})
export class IssueListComponent implements OnInit {

  constructor(private httpClient: HttpClient) { }
  baseUrl: string;
  columnDefs = [
    { field: 'title' },
    { field: 'project' },
    { field: 'priority' },
    { field: 'state' },
    { field: 'creator' },
    { field: 'assignee' },
    { field: 'estimate' },
    { field: 'spent' }
  ];
  rowData: Observable<IssueDto[]>;

  ngOnInit(): void {
    this.baseUrl = `${environment.apiUrl}/issue`;
    this.rowData = this.getIssues();
  }
  getIssues(): Observable<any>{
    return this.httpClient.get(this.baseUrl);
  }
}
