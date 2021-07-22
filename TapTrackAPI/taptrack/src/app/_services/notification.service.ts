import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {InvitationDetailedDto} from "../_interfaces/invitationDto";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private httpClient: HttpClient) {
  }


  getInvitations(): Observable<InvitationDetailedDto[]> {
    return this.httpClient.get<InvitationDetailedDto[]>(`${environment.apiUrl}/invitation/user`);
  }

  getInvitationsCount(): Observable<number> {
    return this.httpClient.get<number>(`${environment.apiUrl}/invitation/count`);
  }

  resolveInvitation(id: string, isAccept: boolean): Observable<any> {
    return this.httpClient.put(`${environment.apiUrl}/invitation/resolveInvitation`, {
      id, isAccept
    });
  }
}
