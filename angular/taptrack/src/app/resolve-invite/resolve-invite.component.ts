import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Params, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-resolve-invite',
  template: ''
})
export class ResolveInviteComponent implements OnInit {

  private invitationId: string;
  private isAccept: string;
  constructor(private router: Router,
              private route: ActivatedRoute,
              private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(query => {
      this.invitationId = query.InvitationId;
      this.isAccept = query.IsAccept;
    });
    this.httpClient.post(`${environment.apiUrl}/Invitation/AcceptOrDeclineInvitation`, {
      InvitationId: this.invitationId,
      IsAccept: this.isAccept
    })
      .subscribe(x => this.router.navigate([`project/list`]));
  }

}
