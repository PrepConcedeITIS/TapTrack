import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {IssueDetailsDto} from "../IssueDetailsDto";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {DropdownsSchemaDto} from "../dropdownsSchemaDto";
import {IssueService} from "../../_services/issue.service";
import {MatDialog} from "@angular/material/dialog";
import {ConfirmDialogComponent} from "../../confirm-dialog/confirm-dialog.component";
import {DateBeautifierService} from "../../_services/date-beautifier.service";

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
              private issueService: IssueService,
              public dialog: MatDialog,
              dateBeautifier: DateBeautifierService) {
    this.dateBeautifier = dateBeautifier;
  }
  dateBeautifier: DateBeautifierService;
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
      console.log(issueData);
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
    this.issueService.editSpentTime(this.issueId, this.issueData.spent)
      .subscribe();
  }

  changeEstimation(){
    this.issueService.editEstimation(this.issueId, this.issueData.estimate)
      .subscribe();
  }

  changeState(){
    this.issueService.editState(this.issueId, this.issueData.state)
      .subscribe();
  }

  redirectToProject(){
    this.router.navigate([`project/details/${this.issueData.projectId}`]);
  }
  edit(){
    this.router.navigate([`issue/edit/${this.issueId}`]);
  }
  delete(){
    const confirmDialog = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Delete Issue?',
        message: 'Are you sure, you want to remove an issue?'
      }
    });
    confirmDialog.afterClosed().subscribe(result => {
      if (result){
        this.httpClient.delete<any>(this.baseUrl + `${this.issueId}`).subscribe(x =>
          this.router.navigate([`issue/list`])
        );
      }
    });
  }
}
