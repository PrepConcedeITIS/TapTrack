import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {User} from './authentication.service';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {IProjectQuery} from '../project/project-create.component';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private httpClient: HttpClient) {
  }

  getSchema(): Observable<any> {
    return of(null);
  }

  createNewProject(project: IProjectQuery): Observable<any> {
    const formData = new FormData();
    formData.append('file', project.logo[0], project.logo[0].name);
    return this.httpClient.post<User>(`${environment.apiUrl}/project`, {
      formDataContent: formData,
      name: project.name
    });
  }
}
