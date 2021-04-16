import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-project-server-errors',
  templateUrl: './project-server-errors.component.html'
})
export class ProjectServerErrorsComponent implements OnInit {

  errors: string[];

  @Input() serverValidationErrors: string;

  constructor() {
  }

  ngOnInit(): void {
    this.errors = this.parseErrors(this.serverValidationErrors);
  }

  parseErrors(errors: string): string[] {
    if (errors === undefined || errors === null) {
      return;
    }
    const message = [];
    errors.split('--').forEach(((value, index) => {
      const error = value.split(':')[1];
      message.push(error);
    }));
    console.log(message);
    return message;
  }
}
