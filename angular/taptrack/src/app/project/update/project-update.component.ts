import {Component, OnInit} from '@angular/core';
import {FormlyFieldConfig} from '@ngx-formly/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ProjectQuery} from '../_interfaces/project-query';
import {BehaviorSubject, Observable, of, timer} from 'rxjs';
import {debounce, skip, tap} from 'rxjs/operators';
import {ProjectService} from '../../_services/project.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-project-update',
  templateUrl: './project-update.component.html',
  styleUrls: ['./project-update.component.scss']
})
export class ProjectUpdateComponent implements OnInit {

  idVisibleSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
  isUnique = true;
  serverValidationErrors: string = null;
  imageSrcOnUpdate = undefined;

  private projectId: string;

  constructor(private projectService: ProjectService,
              private route: ActivatedRoute,
              private router: Router) {
  }

  form = new FormGroup({});
  emailFormControl = new FormControl('', [
    Validators.email,
  ]);
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
      id: 'idVisible',
      templateOptions: {
        label: 'Project Shortcut Name',
        placeholder: 'Enter your new project shortcut name',
        required: true,
        maxLength: 7,
        hideRequiredMarker: true,
        // todo: discuss
        // keyup: (field, event) => {this.idVisibleSubject.next(event.target.value);}
      },
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

<<<<<<< HEAD
  showLogo(field: FormlyFieldConfig, event: any) {
    const fileReader = new FileReader();
    fileReader.onload = () => {
      this.imageSrcOnUpdate = fileReader.result;
    };
    fileReader.readAsDataURL(event.target.files[0]);
  }
=======
  columnDefs = [
    { field: 'Email' },
    { field: 'Status'}
  ];

  defaultColDef = {
    resizable: false,
  };
  rowData: Observable<any>;
>>>>>>> f6cd1613982035a3f2c2c3df8b5b05750ab047fe

  ngOnInit(): void {

    this.route.params.subscribe((params: Params) => {
      this.projectId = params.id;
    });

    this.projectService.getProjectById(this.projectId)
      .subscribe(x => {
        this.model = {description: x.description, idVisible: x.idVisible, logo: undefined, name: x.name};
        this.imageSrcOnUpdate = x.logoUrl;
      });

    this.idVisibleSubject.pipe(skip(1), debounce(() => timer(2000)))
      .subscribe(x => {
        this.projectService.checkForShortIdAvailability(x)
          .subscribe(isUnique => {
            return this.isUnique = isUnique;
          });
      });
    this.rowData = of([]);
  }

  onGridReady(params){
    params.columnApi.sizeColumnsToFit();
  }

  projectGeneralInfoSubmit() {
    this.projectService.updateProject(this.model, this.projectId)
      .pipe(tap((project) => {
        this.router.navigate([`project/details/${project.id}`]);
      }, (err: HttpErrorResponse) => {
        switch (err.status) {
          case 422: {
            this.serverValidationErrors = err.error;
          }
        }
      }))
      .subscribe();
  }

  removeServerErrors() {
    if (this.form.invalid) {
      return;
    }
    this.serverValidationErrors = undefined;
  }
}
