import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {BehaviorSubject, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RestorationService {
  emailBehaviorSubject = new BehaviorSubject<string>('');
  codeBehaviorSubject = new BehaviorSubject<number>(0);

  constructor(private httpClient: HttpClient) {
  }

  sendEmail(userCredentials): Observable<any> {
    return this.httpClient.post(`${environment.apiUrl}/Restoration`, userCredentials);
  }

  sendCode(userCredentials): Observable<any> {
    return this.httpClient.post(`${environment.apiUrl}/Restoration/CheckCode`, userCredentials);
  }

  sendPassword(userCredentials): Observable<any> {
    return this.httpClient.post(`${environment.apiUrl}/Restoration/Password`, userCredentials);
  }
}

export interface RestorationCode {
  userMail: string;
  userCode: number;
  userPassword: string;
}


