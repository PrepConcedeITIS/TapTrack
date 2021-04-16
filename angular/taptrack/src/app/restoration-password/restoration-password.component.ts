import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from "../_services/authentication.service";
import {RestorationService} from "../_services/restoration.service";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {MustMatch} from "../_helpers/must-match.validator";

@Component({
  selector: 'app-restoration-password',
  templateUrl: './restoration-password.component.html',
  styleUrls: ['./restoration-password.component.scss']
})
export class RestorationPasswordComponent implements OnInit {
  form: FormGroup;
  passwordControl: AbstractControl;
  Email: string;
  Code: number;
  Exception: string;
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
      this.restorationService.sbjcode.subscribe((code) => this.Code = code);
      console.log(this.Code);
      this.restorationService.sbjemail.subscribe((Email) => this.Email = Email);
      console.log(this.Email);
      this.restorationService.SendPassword({UserMail: this.Email, UserCode: this.Code, UserPassword: this.form.get('password').value})
        .subscribe((response) => {
            this.router.navigate(['login']);
          },
          (error) => {
            this.Exception = 'Неправильный пароль';
          }
        );
    }
  }
}
