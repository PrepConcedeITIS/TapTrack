import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {IssueDto} from "../issueDto";
import {Router} from '@angular/router';


@Component({
  selector: 'app-issue-list',
  templateUrl: './issue-list.component.html',
  styleUrls: ['./issue-list.component.scss']
})
export class IssueListComponent implements OnInit {

  constructor(private httpClient: HttpClient, private router: Router) { }
  baseUrl: string;
  columnDefs = [
    { field: 'title', width: 250 },
    { field: 'project', width: 250 },
    { field: 'priority', width: 150 },
    { field: 'state', width: 150 },
    { field: 'creator', width: 150 },
    { field: 'assignee', width: 150 },
    { field: 'estimate', width: 150 },
    { field: 'spent', width: 150 }
  ];
  rowData: Observable<IssueDto[]>;

  ngOnInit(): void {
    this.baseUrl = `${environment.apiUrl}/issue`;
    this.rowData = this.getIssues();
  }
  getIssues(): Observable<any>{
    return this.httpClient.get(this.baseUrl);
  }
  goToDetails($event: any) {
    this.router.navigate([`issue/${$event.data.id}`]);
  }
}
