import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MustMatch} from '../_helpers/must-match.validator';
import {Router} from '@angular/router';
import {AuthenticationService} from '../_services/authentication.service';
import {RegistrationService} from '../_services/registration.service';
import {RestorationService} from "../_services/restoration.service";

@Component({
  selector: 'app-restoration',
  templateUrl: './restoration-code.component.html',
  styleUrls: ['./restoration-code.component.scss']
})
export class RestorationCodeComponent implements OnInit {
  form: FormGroup;
  emailControl: AbstractControl;
  // tslint:disable-next-line:new-parens
  constructor(private registrationService: RegistrationService,
              private authenticationService: AuthenticationService,
              private restorationService: RestorationService,
              private formBuilder: FormBuilder,
              private router: Router) {
  }

  ngOnInit(): void {
    this.authenticationService.logout();
    this.form = this.formBuilder.group({
      email: ['', [Validators.email, Validators.required]]
    }, {
    });
    this.emailControl = this.form.get('email');
  }

  submit() {
    if (!this.form.invalid){
      this.restorationService.SendCode({UserMail: this.form.get('email').value, Code: this.form.get('code').value})
        .subscribe();
    }
  }
}
