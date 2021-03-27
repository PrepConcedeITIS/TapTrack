import {Component, OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {ProjectService} from '../../_services/project.service';

@Component({
  selector: 'app-project',
  templateUrl: './project-create.component.html',
  styleUrls: ['./project-create.component.scss']
})
export class ProjectCreateComponent implements OnInit {

  constructor(private projectService: ProjectService) {
  }

  form = new FormGroup({});
  model: IProjectQuery = {description: '', idVisible: '', logo: undefined, name: ''};
  fields: FormlyFieldConfig[] = [
    {
      key: 'name',
      type: 'input',
      className: '',
      templateOptions: {
        label: 'Project Name',
        placeholder: 'Enter your new project name',
        required: true,
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
        hideRequiredMarker: true
      }
    },
    {
      key: 'description',
      type: 'textarea',
      templateOptions: {
        label: 'Project Description',
        placeholder: 'Your project description',
        required: false
      }
    },
    {
      key: 'logo',
      type: 'file',
      templateOptions: {
        label: 'Project logo'
      }
    }
  ];

  ngOnInit(): void {
  }

  onSubmit() {
    console.log(this.model);
    this.projectService.createNewProject(this.model).subscribe(x => console.log(x));
  }
}

export interface IProjectQuery {
  name: string;
  idVisible: string;
  description: string;
  logo: FileList;
}
