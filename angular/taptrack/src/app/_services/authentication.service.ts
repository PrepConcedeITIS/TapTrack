import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  public currentUserSubject: BehaviorSubject<UserWithToken>;
  public currentUser: Observable<UserWithToken>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<UserWithToken>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserWithToken {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string): Observable<any> {
    return this.http.post<UserWithToken>(`${environment.apiUrl}/auth/login`,
      {
        email,
        password
      }
    )
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}

export interface UserWithToken {
  token: string;
  user: User;
}

export interface User {
  id: number;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
}
