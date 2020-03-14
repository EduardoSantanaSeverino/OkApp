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
  XXXEntitySingularXXXServiceProxy,
  XXXEntitySingularXXXDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'edit-XXXEntityLowerSingularXXX-dialog.component.html',
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
export class EditXXXEntitySingularXXXDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  XXXEntityLowerSingularXXX: XXXEntitySingularXXXDto = new XXXEntitySingularXXXDto();
  
  constructor(
    injector: Injector,
    private _XXXEntityLowerSingularXXXService: XXXEntitySingularXXXServiceProxy,
    private _dialogRef: MatDialogRef<EditXXXEntitySingularXXXDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
  ) {
    super(injector);
  }

  ngOnInit(): void {
      this._XXXEntityLowerSingularXXXService
      .get(this._id)
      .subscribe(result => {
          this.XXXEntityLowerSingularXXX = result;
      });
  }

  save(): void {
    this.saving = true;

    this._XXXEntityLowerSingularXXXService
      .update(this.XXXEntityLowerSingularXXX)
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
