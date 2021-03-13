import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor() {
  }

  getSchema(): Observable<any> {
    return of(null);
  }

  createNewProject(project: any): Observable<any> {
    
  }
}
