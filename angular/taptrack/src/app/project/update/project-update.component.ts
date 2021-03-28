import {Component, OnInit} from '@angular/core';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {FormGroup} from '@angular/forms';
import {ProjectQuery} from '../_interfaces/project-query';
import {BehaviorSubject, timer} from 'rxjs';
import {debounce, skip} from 'rxjs/operators';
import {ProjectService} from '../../_services/project.service';
import {ActivatedRoute, Params} from '@angular/router';

@Component({
  selector: 'app-project-update',
  templateUrl: './project-update.component.html',
  styleUrls: ['./project-update.component.scss']
})
export class ProjectUpdateComponent implements OnInit {

  idVisibleSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
  isUnique = true;

  private projectId: string;

  constructor(private projectService: ProjectService,
              private route: ActivatedRoute) {
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
        hideRequiredMarker: true
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
        hideRequiredMarker: true,
        keyup: (field, event) => {
          this.idVisibleSubject.next(event.target.value);
        }
      },
      // validators: {
      //   unique: {
      //     expression: (c) => {
      //       return this.isUnique;
      //     },
      //     message: (e, f) => 'Project with same shortcut name already exist'
      //   }
      // }
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

    this.route.params.subscribe((params: Params) => {
      this.projectId = params.id;
    });

    this.projectService.getProjectById(this.projectId)
      .subscribe(x => {
        this.model = {description: x.description, idVisible: x.idVisible, logo: undefined, name: x.name};
      });

    this.idVisibleSubject.pipe(skip(1), debounce(() => timer(2000)))
      .subscribe(x => {
        this.projectService.checkForShortIdAvailability(x)
          .subscribe(isUnique => {
            return this.isUnique = isUnique;
          });
      });
  }

  projectGeneralInfoSubmit() {
    this.projectService.updateProject(this.model, this.projectId)
      .subscribe();
  }
}
