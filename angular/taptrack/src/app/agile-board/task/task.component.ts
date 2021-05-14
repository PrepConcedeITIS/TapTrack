import { Component, Input, OnInit } from "@angular/core";
import { IssueDetailsDto } from "src/app/issue/issue-details/IssueDetailsDto";


@Component({
    selector: 'app-task',
    templateUrl: './task.component.html',
    styleUrls: ['./task.component.scss']
})

export class TaskComponent implements OnInit{

    @Input() task: IssueDetailsDto;


    ngOnInit(): void {
        
    }
}