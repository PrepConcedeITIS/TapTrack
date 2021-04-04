import { Component, OnInit } from '@angular/core';
import {ProjectService} from '../../_services/project.service'

export interface IProjectItem {
  name: string;
  description: string;
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

  constructor(private projectService: ProjectService) { }

  public projects: IProjectItem[];

  ngOnInit(): void {

    this.projectService.getProjectsList()
      .subscribe((projects: IProjectItem[]) => {
      this.projects = projects;
  }); 
  }

}
