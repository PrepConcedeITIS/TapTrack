import {Component, OnDestroy, OnInit} from '@angular/core';
import {Observable, Subscription} from "rxjs";
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.scss']
})
export class ArticleDetailsComponent implements OnInit, OnDestroy {
  article: Observable<FullArticle>;
  sub: Subscription;

  constructor(private http: HttpClient, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.article = this.http.get<FullArticle>(environment.apiUrl + `/article/${params.id}`);
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}

interface FullArticle {
  projectTitle: string;
  title: string;
  createdBy: TeamMember;
  createdAt: Date;
  updatedBy: TeamMember;
  updatedAt: Date;
  content: string;
}

interface TeamMember {
  username: string;
  email: string;
}
