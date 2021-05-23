import {Injectable} from "@angular/core";
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class IssueService{

  constructor(private httpClient: HttpClient) {
  }
  editState(Id: string, state: string): Observable<any>{
    return this.httpClient.put(`${environment.apiUrl}/issue/state`, {
      Id,
      state
    });
  }

  editPriority(Id: string, priority: string): Observable<any>{
    return this.httpClient.put(`${environment.apiUrl}/issue/priority`, {
      Id,
      priority
    });
  }

  editAssignee(Id: string, assignee: string): Observable<any>{
    return this.httpClient.put(`${environment.apiUrl}/issue/assignee`, {
      Id,
      assignee
    });
  }

  editSpentTime(Id: string, spent: string): Observable<any>{
    return this.httpClient.put(`${environment.apiUrl}/issue/spent`, {
      Id,
      spent
    });
  }

  editEstimation(Id: string, estimation: string): Observable<any>{
    return this.httpClient.put(`${environment.apiUrl}/issue/estimation`, {
      Id,
      estimation
    });
  }

  editType(Id: string, issueType: string): Observable<any>{
    return this.httpClient.put(`${environment.apiUrl}/issue/issueType`, {
      Id,
      issueType
    });
  }
}
