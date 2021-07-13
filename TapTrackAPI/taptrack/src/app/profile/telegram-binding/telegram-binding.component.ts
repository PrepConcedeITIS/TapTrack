import {Component, OnInit} from '@angular/core';
import {BsModalRef} from "ngx-bootstrap/modal";
import {FormGroup} from "@angular/forms";
import {FormlyFieldConfig} from "@ngx-formly/core";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {TelegramInfo} from "./telegram.info";
import {environment} from "../../../environments/environment";
import {pipe} from "rxjs";
import {tap} from "rxjs/operators";

@Component({
  selector: 'app-telegram-binding',
  templateUrl: './telegram-binding.component.html'
})
export class TelegramBindingComponent implements OnInit {
  public state: TelegramState = TelegramState.Base;
  form = new FormGroup({});
  model: TelegramInfo = {userId: '', telegramUserName: '', isEnabled: false, isConnected: false};
  fields: FormlyFieldConfig[] = [
    {
      key: 'telegramUserName',
      type: 'input',
      className: '',
      templateOptions: {
        label: 'Telegram Username',
        disabled: true
      }
    }
  ];
  error: string;
  tgBotName: string = environment.tgBotName;

  constructor(public bsModalRef: BsModalRef,
              private httpClient: HttpClient) {
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.httpClient.get<TelegramInfo>(`${environment.apiUrl}/profile/telegramInfo`)
      .subscribe(x => {
        this.model = x;
      });
  }

  isBase(): boolean {
    return this.state === TelegramState.Base;
  }

  changeState() {
    this.state = this.state === TelegramState.Base
      ? TelegramState.RemoveBinding
      : TelegramState.Base;
  }

  disableTelegramNotifications() {
    this.httpClient.put<boolean>(`${environment.apiUrl}/profile/changeNotificationOption`, {}).subscribe(data => {
      this.model.isEnabled = data;
    });
  }

  unlinkTelegram() {
    this.httpClient.delete(`${environment.apiUrl}/profile/telegramInfo`)
      .pipe(tap(next => {
          this.bsModalRef.hide();
          this.error = undefined;
          this.loadData();
        },
        (error: HttpErrorResponse) => {
          if (error.status === 400) {
            this.error = 'We have got troubles with processing your request, try again later';
          }
        }))
      .subscribe();
  }
}


enum TelegramState {
  Base,
  RemoveBinding
}
