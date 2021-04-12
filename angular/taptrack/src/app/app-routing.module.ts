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
import {IssueDetailsComponent} from "./issue/issue-details/issue-details.component";
import {ArticleComponent} from "./article/article.component";
import {ArticleDetailsComponent} from "./article-details/article-details.component";
import {RestorationEmailComponent} from "./restoration-email/restoration-email.component";

const routes: Routes = [
  {path: '', redirectTo: '/project/list', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'restoration-email', component: RestorationEmailComponent},
  {path: 'registration', component: RegistrationComponent},
  {path: 'issue', canActivate: [AuthGuard], children: [
    {path: 'list', component: IssueListComponent, canActivate: [AuthGuard]},
      {path: ':id', component: IssueDetailsComponent, canActivate: [AuthGuard]}
    ]
  },
  {
    path: 'project',  component: ProjectComponent, canActivate: [AuthGuard],
    children: [
      {path: 'list', component: ProjectListComponent, canActivate: [AuthGuard]},
      {path: 'details/:id', component: ProjectDetailsComponent, canActivate: [AuthGuard]},
      {path: 'create', component: ProjectCreateComponent, canActivate: [AuthGuard]},
      {path: 'edit/:id', component: ProjectUpdateComponent, canActivate: [AuthGuard]},
    ]
  },
  {path: 'article', component: ArticleComponent, canActivate: [AuthGuard], children: [
      {path: 'details/:id', component: ArticleDetailsComponent, canActivate: [AuthGuard]}
    ]},
  {path: '**', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
