import {CdkDragDrop, transferArrayItem} from '@angular/cdk/drag-drop';
import {HttpClient} from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {environment} from 'src/environments/environment';
import {IssueOnBoardDto} from './issueOnBoardDto';
import {IssueService} from '../_services/issue.service';
import {Subject} from "rxjs";
import {ProjectService} from "../_services/project.service";
import {Project} from "../project/_interfaces/project";

@Component({
  selector: 'app-agile-board',
  templateUrl: './agile-board.component.html',
  styleUrls: ['./agile-board.component.scss']
})

export class AgileBoardComponent implements OnInit {

  projectId$: Subject<string> = new Subject<string>();
  projectId: string;

  projectList: Project[];

  constructor(private httpClient: HttpClient, private route: ActivatedRoute, private router: Router,
              private issueService: IssueService, private projectService: ProjectService) {
  }

  minor: IssueOnBoardDto[][] = [[], [], [], [], [], [], [], [], []];
  normal: IssueOnBoardDto[][] = [[], [], [], [], [], [], [], [], []];
  major: IssueOnBoardDto[][] = [[], [], [], [], [], [], [], [], []];
  critical: IssueOnBoardDto[][] = [[], [], [], [], [], [], [], [], []];
  showStopper: IssueOnBoardDto[][] = [[], [], [], [], [], [], [], [], []];

  ngOnInit(): void {
    this.projectId$
      .subscribe(projectId => this.getIssue(projectId));
    this.projectService
      .getProjectsList()
      .subscribe(projects => {
        this.projectList = projects;
        if (this.projectList.length > 0) {
          const projectId = this.projectList[0].id;
          this.projectId$.next(projectId);
          this.projectId = projectId;
        }
      });
    this.route.params
      .subscribe((params: Params) =>  this.projectId$.next(params.id));
  }

  changeProject(): void {
    //this.router.navigate([''])
    this.projectId$.next(this.projectId);
  }

  getIssue(projectId: string) {
    return this.httpClient.get<IssueOnBoardDto[]>(`${environment.apiUrl}/issue/board/${projectId}`).subscribe(issues => {
      issues.forEach(issue => {
        switch (issue.priority) {
          case 'Minor':
            this.filterState(this.minor, issue);
            break;
          case 'Normal':
            this.filterState(this.normal, issue);
            break;
          case 'Major':
            this.filterState(this.major, issue);
            break;
          case 'Critical':
            this.filterState(this.critical, issue);
            break;
          case 'ShowStopper':
            this.filterState(this.showStopper, issue);
            break;
        }

      });
    });
  }

  filterState(priority: Array<Array<IssueOnBoardDto>>, issue: IssueOnBoardDto) {
    switch (issue.state) {
      case 'New':
        priority[0] = [];
        priority[0].push(issue);
        break;
      case 'Analyse':
        priority[1] = [];
        priority[1].push(issue);
        break;
      case 'ToDo':
        priority[2] = [];
        priority[2].push(issue);
        break;
      case 'InProgress':
        priority[3] = [];
        priority[3].push(issue);
        break;
      case 'Incomplete':
        priority[4] = [];
        priority[4].push(issue);
        break;
      case 'Review':
        priority[5] = [];
        priority[5].push(issue);
        break;
      case 'InTest':
        priority[6] = [];
        priority[6].push(issue);
        break;
      case 'Acceptance':
        priority[7] = [];
        priority[7].push(issue);
        break;
      case 'Done':
        priority[8] = [];
        priority[8].push(issue);
        break;
    }
  }

  drop(event: CdkDragDrop<IssueOnBoardDto[]>): void {
    if (event.previousContainer === event.container) {
      return;
    }
    const issueId = event.previousContainer.data[event.previousIndex].id;
    this.editState(issueId, event.container.id[1]);
    this.editPriority(issueId, event.container.id[0]);
    transferArrayItem(
      event.previousContainer.data,
      event.container.data,
      event.previousIndex,
      event.currentIndex
    );
  }


  editState(Id: string, state: string): void {
    this.issueService.editState(Id, state).subscribe();
  }


  editPriority(Id: string, priority: string): void {
    this.issueService.editPriority(Id, priority).subscribe();
  }
}
