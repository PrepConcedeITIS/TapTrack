import {Component, Input, OnInit} from '@angular/core';
import {InvitationDetailedDto, InvitationDto} from "../_interfaces/invitationDto";
import {NotificationService} from "../_services/notification.service";


@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {

  @Input() badgeCount = 0;
  invitations: InvitationDetailedDto[];

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

  resolveInvitation($event: MouseEvent, id: string, isAccept: boolean) {
    this.notificationsService.resolveInvitation(id, isAccept)
      .subscribe(_ => this.setupInvitations());
  }
}
