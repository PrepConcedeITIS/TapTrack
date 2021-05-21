import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {IssueDetailsDto} from "../IssueDetailsDto";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {DropdownsSchemaDto} from "../dropdownsSchemaDto";
import {IssueService} from "../../_services/issue.service";

@Component({
  selector: 'app-details-list',
  templateUrl: './issue-details.component.html',
  styleUrls: ['./issue-details.component.scss']
})
export class IssueDetailsComponent implements OnInit {

  private issueId: string;
  constructor(private httpClient: HttpClient,
              private route: ActivatedRoute,
              private router: Router,
              private issueService: IssueService) { }
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
    this.issueService.editType(this.issueId, this.issueData.issueType)
      .subscribe();
  }

  changePriority(){
    this.issueService.editPriority(this.issueId, this.issueData.priority)
      .subscribe();
  }

  changeAssignee(){
    this.issueService.editAssignee(this.issueId, this.issueData.assignee)
      .subscribe();
  }
  changeSpentTime(){
    console.log('changeSpentTime');
  }

  changeEstimation(){
    console.log('changeEstimation');
  }

  changeState(){
    this.issueService.editState(this.issueId, this.issueData.state)
      .subscribe();
  }

  redirectToProject(){
    this.router.navigate([`project/details/${this.issueData.projectId}`]);
  }
}
