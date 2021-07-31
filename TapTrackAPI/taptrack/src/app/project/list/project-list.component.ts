import {Component, OnInit} from '@angular/core';
import {ProjectService} from '../../_services/project.service';
import {Observable} from 'rxjs';
import {ImageFormatterService} from 'src/app/_services/image-formatter.service';
import {Router} from '@angular/router';


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

  constructor(private projectService: ProjectService,
              private router: Router) {
  }

  columnDefs = [
    {
      headerName: '',
      width: 100,
      field: 'logoUrl',
      cellRendererFramework: ImageFormatterService
    },
    {
      headerName: 'Name',
      width: 250,
      field: 'name'
    },
    {
      headerName: 'Description',
      width: 850,
      field: 'description'
    }
  ];

  rowData: Observable<any[]>;

  ngOnInit(): void {
    this.rowData = this.projectService.getProjectsList();
  }

  goToDetails($event: any) {
    this.router.navigate([`project/details/${$event.data.id}`]);
  }
}


