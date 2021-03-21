import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-issue-list',
  templateUrl: './issue-list.component.html',
  styleUrls: ['./issue-list.component.scss']
})
export class IssueListComponent implements OnInit {

  constructor() { }
  columnDefs = [
    { field: 'title' },
    { field: 'project' },
    { field: 'priority' },
    { field: 'state' },
    { field: 'creator' },
    { field: 'assignee' },
    { field: 'estimate' },
    { field: 'spent' }
  ];

  rowData = [
    {title: 'Issue-1', project: 'Project1', priority: 'Major', state: 'New', creator: 'Anna', assignee: 'Anna', estimate: '1d', spent: '4h'}
  ];

  ngOnInit(): void {
  }
}
