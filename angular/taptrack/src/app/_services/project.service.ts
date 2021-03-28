import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {ProjectQuery} from '../project/_interfaces/project-query';
import {Project} from '../project/_interfaces/project';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  private baseUrl = `${environment.apiUrl}/project/`;

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

    if (project.logo !== undefined) {
      formData.set('logo', project.logo[0], project.logo[0].name);
    }

    return this.httpClient.post(this.baseUrl, formData);
  }

  checkForShortIdAvailability(idVisible: string): Observable<boolean> {
    console.log(idVisible, new Date().getSeconds());
    return this.httpClient
      .get<boolean>(`${environment.apiUrl}/project/idVisibleAvailability/${idVisible}`);
  }

  updateProject(project: ProjectQuery, projectId: string): Observable<any> {
    const formData = new FormData();
    const keys = Object.getOwnPropertyNames(project);
    keys.forEach((propertyName) => {
      formData.append(propertyName, project[propertyName]);
    });

    if (project.logo !== undefined) {
      formData.set('logo', project.logo[0], project.logo[0].name);
    }

    return this.httpClient.put(this.baseUrl + `${projectId}/edit`, formData);
  }

  getProjectById(projectId: string): Observable<Project> {
    return this.httpClient.get<Project>(this.baseUrl + `${projectId}/edit`);
  }
}

