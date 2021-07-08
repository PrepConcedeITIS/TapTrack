import {Injectable} from '@angular/core';
import {AppSettings} from "../app-settings";

declare var require: any;
const DateDiff = require('date-diff');

@Injectable({
  providedIn: 'root'
})
export class DateService {

  constructor() {
  }

  correctToMachineTimeZoneFromUTC(date: Date): Date {
    const toCorrect = new Date(Date.parse(date.toString()));
    toCorrect.setHours(toCorrect.getHours() + AppSettings.ServerTimeShiftHours);
    return toCorrect;
  }

  beatify(date: Date): string {
    const now = new Date(Date.now());

    const toBeautify = this.correctToMachineTimeZoneFromUTC(date);

    const diff = new DateDiff(now, toBeautify);
    return this.getDiffString(diff);
  }

  private getDiffString(diff: any): string {
    if (diff.years() >= 1) {
      return `${Math.floor(diff.years())} year${Math.floor(diff.years()) === 1 ? '' : 's'} ago`;
    } else if (diff.months() >= 1) {
      return `${Math.floor(diff.months())} month${Math.floor(diff.months()) === 1 ? '' : 's'} ago`;
    } else if (diff.weeks() >= 1) {
      return `${Math.floor(diff.weeks())} week${Math.floor(diff.weeks()) === 1 ? '' : 's'} ago`;
    } else if (diff.days() >= 1) {
      return `${Math.floor(diff.days())} day${Math.floor(diff.days()) === 1 ? '' : 's'} ago`;
    } else if (diff.hours() >= 1) {
      return `${Math.floor(diff.hours())} hour${Math.floor(diff.hours()) === 1 ? '' : 's'} ago`;
    } else if (diff.minutes() >= 1) {
      return `${Math.floor(diff.minutes())} minute${Math.floor(diff.minutes()) === 1 ? '' : 's'} ago`;
    } else {
      return `${Math.floor(diff.seconds())} second${Math.floor(diff.seconds()) === 1 ? '' : 's'} ago`;
    }
  }
}
