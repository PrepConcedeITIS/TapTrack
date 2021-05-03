import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup} from '@angular/forms';
import {Router} from '@angular/router';
import {AuthenticationService} from '../../../_services/authentication.service';
import {RestorationService} from '../../../_services/restoration.service';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-restoration',
  templateUrl: './restoration-code.component.html',
  styleUrls: ['./restoration-code.component.scss']
})
export class RestorationCodeComponent implements OnInit {
  form: FormGroup;
  codeControl: AbstractControl;
  email: string;
  exception: string;

  constructor(private authenticationService: AuthenticationService,
              private restorationService: RestorationService,
              private formBuilder: FormBuilder,
              private router: Router) {
  }

  ngOnInit(): void {
    this.authenticationService.logout();
    this.form = this.formBuilder.group({
      code: []
    }, {});
    this.codeControl = this.form.get('code').value;
  }

  submit() {
    if (!this.form.invalid) {
      this.restorationService.emailBehaviorSubject.subscribe((mail) => this.email = mail);
      this.restorationService.codeBehaviorSubject.next(this.form.get('code').value);
      this.restorationService.sendCode({userEmail: this.email, userCode: this.form.get('code').value})
        .subscribe(_ => {
            this.router.navigate(['restoration-password']);
          },
          (error: HttpErrorResponse) => {
            this.exception = 'Incorrect code';
          }
        );
    }
  }
}
