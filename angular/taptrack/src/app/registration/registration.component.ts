import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MustMatch} from '../_helpers/must-match.validator';
import {Router} from '@angular/router';
import {AuthenticationService} from '../_services/authentication.service';
import {RegistrationService} from '../_services/registration.service';
import {tap} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  form: FormGroup;
  emailControl: AbstractControl;

  constructor(private registrationService: RegistrationService,
              private authenticationService: AuthenticationService,
              private formBuilder: FormBuilder,
              private router: Router) {
  }

  ngOnInit(): void {
    this.authenticationService.logout();
    const passwordRegex = new RegExp('^(?=.*[\\d])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*])[\\w!@#$%^&*]{6,}$');
    this.form = this.formBuilder.group({
        email: ['', [Validators.email, Validators.required]],
        password: ['', [Validators.required,
          Validators.pattern(passwordRegex)]],
        passwordRepeat: ['', [Validators.required, Validators.pattern(passwordRegex)]]
      },
      {
        validator: [MustMatch('password', 'passwordRepeat')]
      }
    );
    this.emailControl = this.form.get('email');
  }

  submit() {
    if (!this.form.invalid) {
      this.registrationService.register({
        email: this.form.get('email').value,
        password: this.form.get('password').value
      }).pipe(tap((next) => {
          this.router.navigate(['login'], {
            queryParams: {
              login: next.email
            }
          });
        },
        (err: HttpErrorResponse) => {
          switch (err.status) {
            case 400:
              this.emailControl.setErrors({
                unique: 'Email is already used'
              });
              break;
          }
        }))
        .subscribe();
    } else {
      return;
    }
  }

  redirectToLoginPage(): void {
    this.router.navigate(['login']);
  }
}
