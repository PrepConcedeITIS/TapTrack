import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {BehaviorSubject, Observable} from 'rxjs';
import {RestorationCode} from '../auth/password-restoration/restorationCode';

@Injectable({
  providedIn: 'root'
})
export class RestorationService {
  emailBehaviorSubject = new BehaviorSubject<string>('');
  codeBehaviorSubject = new BehaviorSubject<number>(0);

  constructor(private httpClient: HttpClient) {
  }

  sendEmail(userCredentials: RestorationCode): Observable<any> {
    return this.httpClient.post(`${environment.apiUrl}/Restoration`, userCredentials);
  }

  sendCode(userCredentials: RestorationCode): Observable<any> {
    return this.httpClient.post(`${environment.apiUrl}/Restoration/CheckCode`, userCredentials);
  }

  sendPassword(userCredentials: RestorationCode): Observable<any> {
    return this.httpClient.post(`${environment.apiUrl}/Restoration/Password`, userCredentials);
  }
}


