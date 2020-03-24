import { Component, Injector, Inject, OnInit, Optional } from '@angular/core';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatCheckboxChange
} from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
  LanguageServiceProxy,
  LanguageDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'edit-language-dialog.component.html',
  styles: [
    `
      mat-form-field {
        width: 100%;
      }
      mat-checkbox {
        padding-bottom: 5px;
      }
    `
  ]
})
export class EditLanguageDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  language: LanguageDto = new LanguageDto();
  
  constructor(
    injector: Injector,
    private _languageService: LanguageServiceProxy,
    private _dialogRef: MatDialogRef<EditLanguageDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
  ) {
    super(injector);
  }

  ngOnInit(): void {
      this._languageService
      .get(this._id)
      .subscribe(result => {
          this.language = result;
      });
  }

  save(): void {
    this.saving = true;

    this._languageService
      .update(this.language)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close(true);
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}

