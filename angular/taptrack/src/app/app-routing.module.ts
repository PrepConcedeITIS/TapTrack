import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AuthGuard} from './_helpers/auth.guard';
import {LoginComponent} from './login/login.component';
import {RegistrationComponent} from './registration/registration.component';
import {ProjectCreateComponent} from './project/project-create.component';
import {IssueListComponent} from "./issue/issue-list/issue-list.component";

const routes: Routes = [
  {path: '', component: ProjectCreateComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'registration', component: RegistrationComponent},
  {path: 'issue', canActivate: [AuthGuard], children: [
      {path: 'list', component: IssueListComponent, canActivate: [AuthGuard]}
    ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
