import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from './_helpers/auth.guard';
import {LoginComponent} from './login/login.component';
import {RegistrationComponent} from './registration/registration.component';
import {ProjectCreateComponent} from './project/create/project-create.component';
import {IssueListComponent} from './issue/issue-list/issue-list.component';
import {ProjectDetailsComponent} from './project/details/project-details.component';
import {ProjectUpdateComponent} from './project/update/project-update.component';
import {ProjectListComponent} from './project/list/project-list.component';
import {ProjectComponent} from './project/project.component';
import {ErrorComponent} from './error/error.component';

const routes: Routes = [
  {path: '', redirectTo: '/project/list', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'registration', component: RegistrationComponent},
  {path: 'issue/list', component: IssueListComponent, canActivate: [AuthGuard]},
  {
    path: 'project',  component: ProjectComponent, canActivate: [AuthGuard],
    children: [
      {path: 'list', component: ProjectListComponent, canActivate: [AuthGuard]},
      {path: 'details/:id', component: ProjectDetailsComponent, canActivate: [AuthGuard]},
      {path: 'create', component: ProjectCreateComponent, canActivate: [AuthGuard]},
      {path: 'edit/:id', component: ProjectUpdateComponent, canActivate: [AuthGuard]},
    ]
  },


  {path: '**', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
