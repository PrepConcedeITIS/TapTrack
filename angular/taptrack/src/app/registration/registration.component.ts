import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MustMatch} from '../_helpers/must-match.validator';
import {Router} from '@angular/router';
import {AuthenticationService} from '../_services/authentication.service';
import {RegistrationService} from '../_services/registration.service';

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
    this.form = this.formBuilder.group({
      email: ['', [Validators.email, Validators.required]],
      password: ['', Validators.required],
      passwordRepeat: ['', Validators.required]
    }, {
      validator: MustMatch('password', 'passwordRepeat')
    });
    this.emailControl = this.form.get('email');
  }

  submit() {
    if (!this.form.invalid) {
      console.log('here');
      this.registrationService.register({
        email: this.form.get('email').value,
        password: this.form.get('password').value
      })
        .subscribe(next => {
          this.router.navigate(['login'], {
            queryParams: {
              login: next.email
            }
          });
        });
    } else {
      return;
    }
  }
}
