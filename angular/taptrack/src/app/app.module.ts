import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {JwtInterceptor} from './_helpers/jwt.interceptor';
import {LoginComponent} from './auth/login/login.component';
import {RegistrationComponent} from './auth/registration/registration.component';
import {ProjectCreateComponent} from './project/create/project-create.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import {FormlyModule} from '@ngx-formly/core';
import {FormlyBootstrapModule} from '@ngx-formly/bootstrap';
import {FormlyFieldFileComponent} from './_extensions/file-type.component';
import {FileValueAccessor} from './_extensions/file-value-accessor';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import {TabsModule} from 'ngx-bootstrap/tabs';
import {IssueListComponent} from './issue/issue-list/issue-list.component';
import {AgGridModule} from 'ag-grid-angular';
import {ArticleComponent} from './knowledge-base/article/article.component';
import {ArticleDetailsComponent} from './knowledge-base/article-details/article-details.component';
import {ProjectUpdateComponent} from './project/update/project-update.component';
import {ProjectListComponent} from './project/list/project-list.component';
import {ProjectDetailsComponent} from './project/details/project-details.component';
import {ErrorComponent} from './error/error.component';
import {IssueDetailsComponent} from './issue/issue-details/issue-details.component';
import {CollapseModule} from 'ngx-bootstrap/collapse';
import {AccordionModule} from "ngx-bootstrap/accordion";
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {DragDropModule} from '@angular/cdk/drag-drop'
import { AgileBoardComponent } from './agile-board/agile-board.component';
import {ArticleCreateComponent} from './knowledge-base/article-create/article-create.component';
import {LMarkdownEditorModule, MarkdownEditorComponent} from 'ngx-markdown-editor';
import {FormlyFieldMdEditorComponent} from './_extensions/formly-field-md-editor.component';
import {ImageFormatterService} from './_services/image-formatter.service';
import {ForbiddenErrorComponent} from './error/forbidden-error/forbidden-error.component';
import {CommonModule} from '@angular/common';
import {ProjectServerErrorsComponent} from './project/project-server-errors/project-server-errors.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    ProjectCreateComponent,
    FileValueAccessor,
    FormlyFieldFileComponent,
    IssueListComponent,
    IssueDetailsComponent,
    ArticleComponent,
    ArticleDetailsComponent,
    ProjectUpdateComponent,
    ProjectListComponent,
    ProjectDetailsComponent,
    ErrorComponent,
    AgileBoardComponent,
    ArticleCreateComponent,
    FormlyFieldMdEditorComponent,
    ImageFormatterService,
    ForbiddenErrorComponent,
    ProjectServerErrorsComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormlyModule.forRoot({
      validationMessages: [
        {
          name: 'required',
          message: 'Field is required'
        }
      ],
      types: [
        {name: 'file', component: FormlyFieldFileComponent, wrappers: ['form-field']},
        {name: 'md-editor', component: FormlyFieldMdEditorComponent, wrappers: ['form-field']}
      ],
    }),
    FormlyBootstrapModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    AgGridModule.withComponents([ImageFormatterService]),
    TabsModule.forRoot(),
    AgGridModule,
    CollapseModule,
    AccordionModule.forRoot(),
    MatCardModule,
    MatIconModule,
    DragDropModule,
    FormsModule,
    LMarkdownEditorModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
