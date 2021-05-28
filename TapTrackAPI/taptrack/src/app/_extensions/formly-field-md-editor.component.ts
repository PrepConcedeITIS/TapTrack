import {Component} from '@angular/core';
import {FieldType} from '@ngx-formly/core';

@Component({
  selector: 'app-formly-field-md-editor',
  template: `<md-editor
    [formControl]="formControl"
    [formlyAttributes]="field"
    required></md-editor>`
})
export class FormlyFieldMdEditorComponent extends FieldType {
}
