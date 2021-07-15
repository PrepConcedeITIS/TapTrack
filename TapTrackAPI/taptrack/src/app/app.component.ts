import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from './_services/authentication.service';
import {Router} from '@angular/router';
import {UserWithToken} from './auth/userWithToken';
import {NotificationService} from "./_services/notification.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  isAuthPage = false;
  userEmail = '';
  isCollapsed: boolean;
  router: Router;
  notificationsCount: number;

  constructor(private authService: AuthenticationService, private notificationService: NotificationService, router: Router) {
    this.isCollapsed = false;
    this.router = router;
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

    this.notificationService.getInvitationsCount()
      .subscribe(value => this.notificationsCount = value);
  }

  navigateToProfile() {
    this.router.navigate(['profile']);
  }

  signOut() {
    this.router.navigate(['login']);
  }
}
