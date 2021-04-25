import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ProjectService} from '../../_services/project.service';
import {Project} from '../_interfaces/project';
import {tap} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {FormGroup} from '@angular/forms';
import {FormlyFieldConfig} from '@ngx-formly/core';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent implements OnInit {

  project: Project;

  form = new FormGroup({});
  model = {description: '', idVisible: '', name: ''};
  fields: FormlyFieldConfig[] = [
    {
      key: 'name',
      type: 'input',
      className: '',
      templateOptions: {
        label: 'Project Name',
        placeholder: 'Enter your new project name',
        required: true,
        maxLength: 30,
        hideRequiredMarker: true,
        disabled: true
      }
    },
    {
      key: 'idVisible',
      type: 'input',
      id: 'idVisible',
      templateOptions: {
        label: 'Project Shortcut Name',
        placeholder: 'Enter your new project shortcut name',
        required: true,
        maxLength: 7,
        hideRequiredMarker: true,
        disabled: true
      },
    },
    {
      key: 'description',
      type: 'textarea',
      templateOptions: {
        label: 'Project Description',
        placeholder: 'Your project description',
        required: false,
        maxLength: 500,
        disabled: true
      }
    }
  ];

  constructor(private route: ActivatedRoute,
              private projectService: ProjectService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.projectService.getProjectById(params.id)
        .pipe(tap((project) => {
          this.project = project;
          this.model = {description: project.description, idVisible: project.idVisible, name: project.name};
        }, (err: HttpErrorResponse) => {
          switch (err.status) {
            case 422: {
              break;
            }
          }
        }))
        .subscribe();
    });
  }

  goToEdit() {
    this.router.navigate([`project/edit/${this.project.id}`]);
  }
}
