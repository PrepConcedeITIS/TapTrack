import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuard} from './_helpers/auth.guard';
import {LoginComponent} from './auth/login/login.component';
import {RegistrationComponent} from './auth/registration/registration.component';
import {ProjectCreateComponent} from './project/create/project-create.component';
import {IssueListComponent} from './issue/issue-list/issue-list.component';
import {ProjectDetailsComponent} from './project/details/project-details.component';
import {ProjectUpdateComponent} from './project/update/project-update.component';
import {ProjectListComponent} from './project/list/project-list.component';
import {ErrorComponent} from './error/error.component';
import {IssueDetailsComponent} from "./issue/issue-details/issue-details.component";
import {RestorationEmailComponent} from "./restoration-email/restoration-email.component";
import {RestorationCodeComponent} from "./restoration-code/restoration-code.component";
import {RestorationPasswordComponent} from "./restoration-password/restoration-password.component";
import {ArticleComponent} from "./knowledge-base/article/article.component";
import {ArticleDetailsComponent} from "./knowledge-base/article-details/article-details.component";
import {ArticleCreateComponent} from "./knowledge-base/article-create/article-create.component";
import {ForbiddenErrorComponent} from './error/forbidden-error/forbidden-error.component';
import {ArticleUpdateComponent} from "./knowledge-base/article-update/article-update.component";

const routes: Routes = [
  {path: '', redirectTo: '/project/list', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'restoration-email', component: RestorationEmailComponent},
  {path: 'restoration-code', component: RestorationCodeComponent},
  {path: 'restoration-password', component: RestorationPasswordComponent},
  {path: 'registration', component: RegistrationComponent},
  {
    path: 'issue', canActivate: [AuthGuard], children: [
      {path: 'list', component: IssueListComponent, canActivate: [AuthGuard]},
      {path: ':id', component: IssueDetailsComponent, canActivate: [AuthGuard]}
    ]
  },
  {
    path: 'project', canActivate: [AuthGuard],
    children: [
      {path: 'list', component: ProjectListComponent, canActivate: [AuthGuard]},
      {path: 'details/:id', component: ProjectDetailsComponent, canActivate: [AuthGuard]},
      {path: 'create', component: ProjectCreateComponent, canActivate: [AuthGuard]},
      {path: 'edit/:id', component: ProjectUpdateComponent, canActivate: [AuthGuard]},
    ]
  },
  {
    path: 'article', component: ArticleComponent, canActivate: [AuthGuard], children: [
      {path: 'create', component: ArticleCreateComponent, canActivate: [AuthGuard]},
      {path: 'details/:id', component: ArticleDetailsComponent, canActivate: [AuthGuard]},
      {path: 'edit/:id', component: ArticleUpdateComponent, canActivate: [AuthGuard]}
    ]
  },
  {path: 'access-error', component: ForbiddenErrorComponent},
  {path: '**', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
