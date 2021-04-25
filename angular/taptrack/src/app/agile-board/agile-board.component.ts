import { CdkDragDrop, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';

export interface Task {
    id?: string;
    title: string;
    description: string;
}

@Component({
  selector: 'app-agile-board',
  templateUrl: './agile-board.component.html',
  styleUrls: ['./agile-board.component.scss']
})

export class AgileBoardComponent implements OnInit {
    new: Task[] = [];
    todo: Task[] = [
        {title: "TestTitle1", description: "Test Description1 Test Description1 Test Description1"},
        {title: "TestTitle2", description: "Test Description2"},
        {title: "TestTitle3", description: "Test Description3"}
    ]
    inProgress: Task[] = [];
    inTest: Task[] = [];
    review: Task[] = [];
    done: Task[] = [];

    drop(event: CdkDragDrop<Task[]>): void {
        if (event.previousContainer == event.container){
            return;
        }
        transferArrayItem(
            event.previousContainer.data,
            event.container.data,
            event.previousIndex,
            event.currentIndex
        );
    }

    ngOnInit(): void {
        
    }
}