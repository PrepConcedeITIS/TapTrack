import {Component, Input, OnInit} from '@angular/core';
import {InvitationDto} from "../_interfaces/invitationDto";
import {NotificationService} from "../_services/notification.service";


@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {

  @Input() badgeCount = 0;
  invitations: InvitationDto[];

  constructor(private notificationsService: NotificationService) {
  }

  ngOnInit(): void {
  }

  setupInvitations(): void {
    this.notificationsService.getInvitations()
      .subscribe(value => {
        this.invitations = value;
      });
  }
}
