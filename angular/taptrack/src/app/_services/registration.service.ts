import {Injectable, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs';
import {User} from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private httpClient: HttpClient) {
  }

  register(userCredentials: any): Observable<User> {
    return this.httpClient.post<User>(`${environment.apiUrl}/auth/register`, userCredentials);
  }

}
