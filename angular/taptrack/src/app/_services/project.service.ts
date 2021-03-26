import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {User} from './authentication.service';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {ProjectQuery} from '../project/project-query';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private httpClient: HttpClient) {
  }

  getSchema(): Observable<any> {
    return of(null);
  }

  createNewProject(project: ProjectQuery): Observable<any> {
    const formData = new FormData();
    const keys = Object.getOwnPropertyNames(project);
    keys.forEach((propertyName) => {
      formData.append(propertyName, project[propertyName]);
    });

    formData.set('logo', project.logo[0], project.logo[0].name);

    return this.httpClient.post<User>(`${environment.apiUrl}/project`, formData);
  }

  checkForShortIdAvailability(idVisible: string): Observable<boolean> {
    console.log(idVisible, new Date().getSeconds());
    return this.httpClient
      .get<boolean>(`${environment.apiUrl}/project/idVisibleAvailability/${idVisible}`);
  }
}

