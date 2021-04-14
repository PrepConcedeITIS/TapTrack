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
  codeControl: AbstractControl;
  Email: string;
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
      code:[]
    }, {});
    this.codeControl = this.form.get('code').value;
  }

  submit() {
    if (!this.form.invalid){
      this.restorationService.sbj.subscribe((mail) => this.Email = mail);
      this.restorationService.SendCode({UserMail: this.Email, UserCode: this.form.get('code').value})
        .subscribe();
    }
  }
}
