import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from './_services/authentication.service';
import {Router} from '@angular/router';
import {UserWithToken} from './auth/userWithToken';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  isAuthPage = false;
  userEmail = '';
  isCollapsed: boolean;

  constructor(private authService: AuthenticationService,
              private router: Router) {
    this.isCollapsed = false;
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

  navigateToProfile() {
    this.router.navigate(['profile']);
  }

  signOut() {
    this.router.navigate(['login']);
  }
}
