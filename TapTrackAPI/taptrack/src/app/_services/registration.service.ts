import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs';
import {Router} from '@angular/router';
import {User} from '../auth/user';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  register(userCredentials: any): Observable<User> {
    return this.httpClient.post<User>(`${environment.apiUrl}/auth/register`, userCredentials);
  }

}
