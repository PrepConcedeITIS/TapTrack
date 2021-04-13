import {Injectable, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs';
import {User} from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RestorationService {

  constructor(private httpClient: HttpClient) {
  }

  SendEmail(userCredentials): Observable<RestorationCode> {
    return this.httpClient.post<RestorationCode>(`${environment.apiUrl}/Restoration`, userCredentials);
  }

  SendCode(userCredentials): Observable<RestorationCode> {
    return this.httpClient.post<RestorationCode>(`${environment.apiUrl}/Restoration`, userCredentials);
  }
}

export interface RestorationCode{
  UserMail: string;
  Code: number;
}


