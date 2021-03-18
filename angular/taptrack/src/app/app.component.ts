import {Component, OnInit} from '@angular/core';
import {AuthenticationService, UserWithToken} from './_services/authentication.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  isAuthPage = false;
  userEmail = '';

  constructor(private authService: AuthenticationService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.authService.currentUserSubject.subscribe((user: UserWithToken) => {
      setTimeout(() => {
        this.isAuthPage = (user === null);
        if (!this.isAuthPage) {
          this.userEmail = user.user.email;
        }
      }, 0);
    });
  }

  signOut() {
    this.router.navigate(['login']);
  }
}
