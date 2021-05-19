import { CdkDragDrop, transferArrayItem } from '@angular/cdk/drag-drop';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IssueOnBoardDto } from './IssueDetailsDto';
import { TaskComponent } from './task/task.component'

@Component({
    selector: 'app-agile-board',
    templateUrl: './agile-board.component.html',
    styleUrls: ['./agile-board.component.scss']
})

export class AgileBoardComponent implements OnInit {

    private projectId: string;
    constructor(private httpClient: HttpClient, private route: ActivatedRoute) { };
    issueList = Array<IssueOnBoardDto>();

    minor: IssueOnBoardDto[][] = [[],[],[],[],[],[],[],[]];
    normal: IssueOnBoardDto[][] = [[],[],[],[],[],[],[],[]];
    major: IssueOnBoardDto[][] = [[],[],[],[],[],[],[],[]];
    critical: IssueOnBoardDto[][] = [[],[],[],[],[],[],[],[]];
    showStopper: IssueOnBoardDto[][] = [[],[],[],[],[],[],[],[]];


    ngOnInit(): void {
        this.route.params.subscribe((params: Params) => {
            this.projectId = params.id;
        });
        this.getIssue();
    }

    getIssue() {
        return this.httpClient.get<IssueOnBoardDto[]>(`${environment.apiUrl}/issue/board/${this.projectId}`).subscribe(issues => {
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
                priority[0].push(issue);
                break;
            case 'Analyse':
                priority[1].push(issue);
                break;
            case 'ToDo':
                priority[2].push(issue);
                break;
            case 'InProgress':
                priority[3].push(issue);
                break;
            case 'Incomplete':
                priority[4].push(issue);
                break;
            case 'InTest':
                priority[5].push(issue);
                break;
            case 'Acceptance':
                priority[6].push(issue);
                break;
            case 'Done':
                priority[7].push(issue);
                break;
        }
    }

    drop(event: CdkDragDrop<IssueOnBoardDto[]>): void {
        if (event.previousContainer == event.container) {
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
        this.httpClient.put(`${environment.apiUrl}/issue/state`, {
            Id,
            state
        }).subscribe();
    }

    editPriority(Id: string, priority: string): void {
        this.httpClient.put(`${environment.apiUrl}/issue/priority`, {
            Id,
            priority
        }).subscribe();
    }
}