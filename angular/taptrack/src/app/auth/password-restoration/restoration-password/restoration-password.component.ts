import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../../_services/authentication.service';
import {RestorationService} from '../../../_services/restoration.service';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {MustMatch} from '../../../_helpers/must-match.validator';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-restoration-password',
  templateUrl: './restoration-password.component.html',
  styleUrls: ['./restoration-password.component.scss']
})
export class RestorationPasswordComponent implements OnInit {
  form: FormGroup;
  passwordControl: AbstractControl;
  email: string;
  code: number;
  exception: string;

  constructor(private authenticationService: AuthenticationService,
              private restorationService: RestorationService,
              private formBuilder: FormBuilder,
              private router: Router) {
  }

  ngOnInit(): void {
    this.authenticationService.logout();
    this.form = this.formBuilder.group({
      password: ['', Validators.required],
      passwordRepeat: ['', Validators.required]
    }, {
      validator: MustMatch('password', 'passwordRepeat')
    });
    this.passwordControl = this.form.get('password').value;
  }

  submit() {
    if (!this.form.invalid) {
      this.restorationService.codeBehaviorSubject.subscribe((code) => this.code = code);
      console.log(this.code);
      this.restorationService.emailBehaviorSubject.subscribe((Email) => this.email = Email);
      console.log(this.email);
      this.restorationService.sendPassword({userMail: this.email, userCode: this.code, userPassword: this.form.get('password').value})
        .subscribe(_ => {
            this.router.navigate(['login']);
          },
          (error: HttpErrorResponse) => {
            this.exception = 'Incorrect password';
          }
        );
    }
  }
}
