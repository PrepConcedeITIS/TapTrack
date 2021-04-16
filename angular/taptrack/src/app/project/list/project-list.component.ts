import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../_services/project.service';
import { Observable } from "rxjs";
import { ThrowStmt } from '@angular/compiler';
import { ImageFormatterService } from 'src/app/_services/image-formatter.service';

export interface IProject {
  id: string;
  name: string;
  description: string;
  idVisible: string;
  logoUrl: string;
}

export interface IGrouped<TItem> {
  groupLength: number;
  items: TItem[];
}

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {

  public projectsList: IProject[] = [];

  constructor(private projectService: ProjectService) { }

  columnDefs = [
    { headerName: '',
      width: 250,
      field: 'logoUrl',
      cellRendererFramework: ImageFormatterService},
    { headerName: 'Name',
      width: 300,
      field: 'name' },
    { headerName: 'Description',
      width: 600,
      field: 'description' }
  ];

  rowData: Observable<any[]>;

  ngOnInit(): void {
    this.rowData = this.projectService.getProjectsList()
  }
}


