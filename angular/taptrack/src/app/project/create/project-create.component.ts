import {Component, OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {ProjectService} from '../../_services/project.service';
import {ProjectQuery} from '../_interfaces/project-query';
import {Router} from '@angular/router';
import {tap} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';


@Component({
  selector: 'app-project',
  templateUrl: './project-create.component.html',
  styleUrls: ['./project-create.component.scss']
})
export class ProjectCreateComponent implements OnInit {

  serverValidationErrors: string;
  imageSrc = undefined;

  constructor(private projectService: ProjectService, private router: Router) {
  }

  form = new FormGroup({});
  model: ProjectQuery = {description: '', idVisible: '', logo: undefined, name: ''};
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
        hideRequiredMarker: true
      }
    },
    {
      key: 'idVisible',
      type: 'input',
      templateOptions: {
        label: 'Project Shortcut Name',
        placeholder: 'Enter your new project shortcut name',
        required: true,
        maxLength: 7,
        hideRequiredMarker: true
      }
    },
    {
      key: 'description',
      type: 'textarea',
      templateOptions: {
        label: 'Project Description',
        placeholder: 'Your project description',
        required: false,
        maxLength: 500
      }
    },
    {
      key: 'logo',
      type: 'file',
      templateOptions: {
        label: 'Project logo',
        change: ((field, event) => this.showLogo(field, event))
      }
    }
  ];

  showLogo(field: FormlyFieldConfig, event: any) {
    console.log(event.target.files[0]);
    const fileReader = new FileReader();
    fileReader.onload = () => {
      this.imageSrc = fileReader.result;
    };
    fileReader.readAsDataURL(event.target.files[0]);
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.projectService.createNewProject(this.model)
      .pipe(tap((project) => {
          this.router.navigate([`project/details/${project.id}`]);
        },
        (err: HttpErrorResponse) => {
          switch (err.status) {
            case 422: {
              this.serverValidationErrors = err.error;
            }
          }
        }))
      .subscribe();
  }
}

