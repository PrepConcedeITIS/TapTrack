import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {JwtInterceptor} from './_helpers/jwt.interceptor';
import {LoginComponent} from './login/login.component';
import {RegistrationComponent} from './registration/registration.component';
import {ProjectComponent} from './project/project.component';
import {ReactiveFormsModule} from '@angular/forms';

import {FormlyModule} from '@ngx-formly/core';
import {FormlyBootstrapModule} from '@ngx-formly/bootstrap';
import {FormlyFieldFileComponent} from "./_extensions/file-type.component";
import {FileValueAccessor} from "./_extensions/file-value-accessor";
import { ProfileComponent } from './profile/profile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MaterialModule} from "./_modules/material/material.module";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent,
    ProjectComponent,
    FileValueAccessor,
    FormlyFieldFileComponent,
    ProfileComponent,
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormlyModule.forRoot({
      types: [
        { name: 'file', component: FormlyFieldFileComponent, wrappers: ['form-field'] },
      ],
    }),
    FormlyBootstrapModule,
    BrowserAnimationsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
