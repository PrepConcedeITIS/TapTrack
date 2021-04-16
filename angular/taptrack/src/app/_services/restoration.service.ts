import {Injectable, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {BehaviorSubject, Observable} from 'rxjs';
import {User} from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RestorationService {
  sbjemail = new BehaviorSubject<string>("");
  sbjcode = new BehaviorSubject<number>(0);
  constructor(private httpClient: HttpClient) {
  }

  SendEmail(userCredentials): Observable<RestorationCode> {
    return this.httpClient.post<RestorationCode>(`${environment.apiUrl}/Restoration`, userCredentials);
  }

  SendCode(userCredentials): Observable<RestorationCode> {
    return this.httpClient.post<RestorationCode>(`${environment.apiUrl}/Restoration/CheckCode`, userCredentials);
  }

  SendPassword(userCredentials): Observable<RestorationCode>{
    return this.httpClient.post<RestorationCode>('$environment.apiUrl/Restoration/Password', userCredentials);
  }
}

export interface RestorationCode{
  UserMail: string;
  UserCode: number;
  UserPassword: string;
}


