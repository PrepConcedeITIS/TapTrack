import {Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class KnowledgeBaseService {

  needToRefreshArticles = new BehaviorSubject<boolean>(false);

  constructor() {
  }
}
