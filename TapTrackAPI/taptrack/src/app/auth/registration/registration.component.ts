import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MustMatch} from '../../_helpers/must-match.validator';
import {Router} from '@angular/router';
import {AuthenticationService} from '../../_services/authentication.service';
import {RegistrationService} from '../../_services/registration.service';
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
  passwordControl: AbstractControl;

  passwordErrors: ValidationResult<string>[] = [];

  constructor(private registrationService: RegistrationService,
              private authenticationService: AuthenticationService,
              private formBuilder: FormBuilder,
              private router: Router) {
  }

  ngOnInit(): void {
    this.authenticationService.logout();
    const passwordRegex = new RegExp('^(?=.*[\\d])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*])[\\w!@#$%^&*]{7,}$');
    this.form = this.formBuilder.group({
        email: ['', [Validators.email, Validators.required]],
        password: ['', [Validators.required]],
        passwordRepeat: ['', [Validators.required]]
      },
      {
        validator: [MustMatch('password', 'passwordRepeat')]
      }
    );
    this.emailControl = this.form.get('email');
    this.passwordControl = this.form.get('password');
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


  validatePassword() {
    const errorList = this.getPasswordPatternErrors(this.form.get('password').value).filter(v => !v.isSuccess);

    if (errorList.length !== 0) {
      this.passwordControl.setErrors({});
    }

    this.passwordErrors = errorList;
    errorList.filter(value => !value.isSuccess).forEach(value => {
      this.passwordControl.errors[value.validationKey] = value.message;
    });
  }


  private getPasswordPatternErrors(input: string): ValidationResult<string>[] {
    const result: ValidationResult<string>[] = [];
    const letterRegex = new RegExp(/[a-zA-Z]/);
    let hasSymbol = false;
    let hasLowercase = false;
    let hasUppercase = false;
    let hasDigit = false;
    for (const character of input) {
      if (!isNaN(character as any)) {
        hasDigit = true;
      } else if (letterRegex.test(character)) {
        if (!hasUppercase && character.toUpperCase() !== character) {
          hasUppercase = true;
        } else if (!hasLowercase) {
          hasLowercase = true;
        }
      } else if (!hasSymbol) {
        hasSymbol = true;
      }
    }

    if (!hasSymbol) {
      result.push(new ValidationResult<string>(input, false, 'hasSymbol', 'Password should contain at least one special character'));
    }
    if (!hasLowercase) {
      result.push(new ValidationResult<string>(input, false, 'hasLowercase', 'Password should contain at least one lowercase letter'));
    }
    if (!hasUppercase) {
      result.push(new ValidationResult<string>(input, false, 'hasUppercase', 'Password should contain at least one uppercase letter'));
    }
    if (!hasDigit) {
      result.push(new ValidationResult<string>(input, false, 'hasDigit', 'Password should contain at least one digit'));
    }
    if (input.length < 7) {
      result.push(new ValidationResult<string>(input, false, 'length', 'Password minimum length is 7 characters,' +
        ` you should add ${7 - input.length} more`));
    }

    return result;
  }
}

export class ValidationResult<T> {
  value: T;
  isSuccess: boolean;
  validationKey: string;
  message: string;

  constructor(value: T, isSuccess: boolean, validationKey: string, message: string) {
    this.value = value;
    this.isSuccess = isSuccess;
    this.validationKey = validationKey;
    this.message = message;
  }
}
