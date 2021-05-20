import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {IssueDetailsDto} from "../IssueDetailsDto";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {DropdownsSchemaDto} from "../dropdownsSchemaDto";

@Component({
  selector: 'app-details-list',
  templateUrl: './issue-details.component.html',
  styleUrls: ['./issue-details.component.scss']
})
export class IssueDetailsComponent implements OnInit {

  private issueId: string;
  constructor(private httpClient: HttpClient,
              private route: ActivatedRoute,
              private router: Router) { }
  baseUrl: string;
  fields: FormlyFieldConfig[];
  issueData: IssueDetailsDto;
  dropdownsSchema: DropdownsSchemaDto;

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.issueId = params.id;
    });
    this.baseUrl = `${environment.apiUrl}/issue/`;
    this.getIssue().subscribe(issueData => {
      this.issueData = issueData;
      this.httpClient.get<DropdownsSchemaDto>(`${this.baseUrl}Dropdowns/${issueData.projectId}`)
        .subscribe(data => {
          this.dropdownsSchema = data;
          this.dropdownsSchema.assignee.push('Unassigned');
          if (!issueData.assignee) {
            issueData.assignee = 'Unassigned';
          }
        });
    });
  }

  getIssue(): Observable<IssueDetailsDto>{
    return this.httpClient.get<IssueDetailsDto>(this.baseUrl + this.issueId);
  }

  changeIssueType(){
    console.log('changeIssueType');
  }

  changePriority(){
    console.log('changePriority');
  }

  changeAssignee(){
    console.log('changeAssignee');
  }
  changeSpentTime(){
    console.log('changeSpentTime');
  }

  changeEstimation(){
    console.log('changeEstimation');
  }

  changeState(){
    console.log('changeState');
  }

  redirectToProject(){
    this.router.navigate([`project/details/${this.issueData.projectId}`]);
  }
}
