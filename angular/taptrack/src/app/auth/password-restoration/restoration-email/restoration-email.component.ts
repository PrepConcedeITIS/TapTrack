import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {AuthenticationService} from '../../../_services/authentication.service';
import {RegistrationService} from '../../../_services/registration.service';
import {RestorationService} from "../../../_services/restoration.service";

@Component({
  selector: 'app-restoration',
  templateUrl: './restoration-email.component.html',
  styleUrls: ['./restoration-email.component.scss']
})

export class RestorationEmailComponent implements OnInit {
  form: FormGroup;
  emailControl: AbstractControl;

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
    }, {});
    this.emailControl = this.form.get('email');
  }

  submit() {
    if (!this.form.invalid) {
      this.restorationService.emailBehaviorSubject.next(this.form.get('email').value);
      this.restorationService.sendEmail({userMail: this.form.get('email').value})
        .subscribe(x =>
          this.router.navigate(['restoration-code']));
    }
  }
}
