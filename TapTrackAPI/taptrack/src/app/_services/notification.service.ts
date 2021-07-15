import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {InvitationDto} from "../_interfaces/invitationDto";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private httpClient: HttpClient) {
  }


  getInvitations(): Observable<InvitationDto[]> {
    return this.httpClient.get<InvitationDto[]>(`${environment.apiUrl}/invitation/user`);
  }

  getInvitationsCount(): Observable<number> {
    return this.httpClient.get<number>(`${environment.apiUrl}/invitation/count`);
  }
}
