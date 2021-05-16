import { CdkDragDrop, transferArrayItem } from '@angular/cdk/drag-drop';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IssueOnBoardDto } from './IssueDetailsDto';

@Component({
    selector: 'app-agile-board',
    templateUrl: './agile-board.component.html',
    styleUrls: ['./agile-board.component.scss']
})

export class AgileBoardComponent implements OnInit {

    private projectId: string;
    constructor(private httpClient: HttpClient, private route: ActivatedRoute) { };
    issueList = Array<IssueOnBoardDto>();
    new: IssueOnBoardDto[] = [];
    analyse: IssueOnBoardDto[] = [];
    todo: IssueOnBoardDto[] = [];
    inProgress: IssueOnBoardDto[] = [];
    incomplete: IssueOnBoardDto[] = [];
    inTest: IssueOnBoardDto[] = [];
    acceptance: IssueOnBoardDto[] = [];
    done: IssueOnBoardDto[] = [];


    ngOnInit(): void {
        this.route.params.subscribe((params: Params) => {
            this.projectId = params.id;
        });
        this.getIssue();
    }

    getIssue() {
        return this.httpClient.get<IssueOnBoardDto[]>(`${environment.apiUrl}/issue/board/${this.projectId}`).subscribe(issues => {
            issues.forEach(issue => {
                switch (issue.state) {
                    case 'New':
                        this.new.push(issue);
                        break;
                    case 'Analyse':
                        this.analyse.push(issue);
                        break;
                    case 'ToDo':
                        this.todo.push(issue);
                        break;
                    case 'InProgress':
                        this.inProgress.push(issue);
                        break;
                    case 'Incomplete':
                        this.incomplete.push(issue);
                        break;
                    case 'InTest':
                        this.inTest.push(issue);
                        break;
                    case 'Acceptance':
                        this.acceptance.push(issue);
                        break;
                    case 'Done':
                        this.done.push(issue);
                        break;
                }
            });
        });
    }



    drop(event: CdkDragDrop<IssueOnBoardDto[]>): void {
        if (event.previousContainer == event.container) {
            return;
        }
        const issueId = event.previousContainer.data[event.previousIndex].id;
        this.editState(issueId, event.container.id)
        transferArrayItem(
            event.previousContainer.data,
            event.container.data,
            event.previousIndex,
            event.currentIndex
        );
    }

    editState(Id: string, state: string): void{
        this.httpClient.put(`${environment.apiUrl}/issue/state`, {
            Id,
            state
          }).subscribe();
    }

    editPriority(Id: string, priority: string): void{
        this.httpClient.put(`${environment.apiUrl}/issue/priority`, {
            Id,
            priority
          }).subscribe();
    }
}