import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {IssueDetailsDto} from "./IssueDetailsDto";
import {ActivatedRoute, Params} from "@angular/router";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {first} from "rxjs/operators";

@Component({
  selector: 'app-details-list',
  templateUrl: './issue-details.component.html',
  styleUrls: ['./issue-details.component.scss']
})
export class IssueDetailsComponent implements OnInit {

  private issueId: string;
  constructor(private httpClient: HttpClient, private route: ActivatedRoute) { }
  baseUrl: string;
  fields: FormlyFieldConfig[];
  issueData: Observable<IssueDetailsDto>;

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.issueId = params.id;
    });
    this.baseUrl = `${environment.apiUrl}/issue/${this.issueId}`;
    this.issueData = this.getIssue();
  }

  getIssue(): Observable<IssueDetailsDto>{
    return this.httpClient.get<IssueDetailsDto>(this.baseUrl);
  }

}
