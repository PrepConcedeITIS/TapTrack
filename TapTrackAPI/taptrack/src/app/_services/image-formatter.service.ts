import {Component} from '@angular/core';

@Component({
  selector: 'app-image-formatter-cell',
  template: `<img border="0" width="40" height="40" src=\"{{ params.value }}\">`
})

export class ImageFormatterService {
  params: any;

  agInit(params: any) {
    this.params = params;
  }
}
