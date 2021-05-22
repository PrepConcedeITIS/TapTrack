import { Component, Input, OnInit } from "@angular/core";
import {IssueDetailsDto} from '../../issue/IssueDetailsDto';


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
