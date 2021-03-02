import { Component, OnInit } from '@angular/core';
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  constructor() { }
  form = new FormGroup({});
  model = { email: 'email@gmail.com' };
  fields: FormlyFieldConfig[] = [
    {
      key: 'name',
      type: 'input',
      templateOptions: {
        label: 'Project Name',
        placeholder: 'Enter your new project name',
        required: true,
      }
    },
    {
      key: 'idVisible',
      type: 'input',
      templateOptions: {
        label: 'Project Shortcut Name',
        placeholder: 'Enter your new project shortcut name',
        required: true,
      }
    },
    {
      key: 'description',
      type: 'textarea',
      templateOptions: {
        label: 'Project Description',
        placeholder: 'Your project description',
        required: false,
      }
    },
    {
      key: 'logo',
      type: 'file'
    }
  ];

  ngOnInit(): void {
  }

  onSubmit() {
    console.log(this.model);
  }
}
