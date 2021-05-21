import {Component, OnInit} from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {first} from 'rxjs/operators';
import {AuthenticationService} from '../../_services/authentication.service';
import {HttpErrorResponse} from '@angular/common/http';


@Component({templateUrl: 'login.component.html'})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';
  login: string;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
  }

  ngOnInit(): void {
    this.authenticationService.logout();
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
    this.login = this.route.snapshot.queryParams.login || '';

    this.loginForm = this.formBuilder.group({
      email: [this.login, Validators.required],
      password: ['', Validators.required]
    });
  }

  // convenience getter for easy access to form fields
  get formControls() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authenticationService.login(this.formControls.email.value, this.formControls.password.value)
      .pipe(first())
      .subscribe(
        _ => {
          this.router.navigate([this.returnUrl]);
        },
        (error: HttpErrorResponse) => {
          switch (error.status) {
            case 400:
              this.error = 'Incorrect login or password';
          }
          this.loading = false;
        });
  }

  redirectToRegistration(): void {
    this.router.navigate(['registration']);
  }

  clearError() {
    this.error = undefined;
  }
}
