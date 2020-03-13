import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
  XXXEntitySingularXXXServiceProxy,
  XXXEntitySingularXXXDto,
  XXXEntitySingularXXXCreateDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-XXXEntityLowerSingularXXX-dialog.component.html',
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
export class CreateXXXEntitySingularXXXDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  XXXEntityLowerSingularXXX: XXXEntitySingularXXXDto = new XXXEntitySingularXXXDto();

  constructor(
	injector: Injector,
	private _XXXEntityLowerSingularXXXService: XXXEntitySingularXXXServiceProxy,
	private _dialogRef: MatDialogRef<CreateXXXEntitySingularXXXDialogComponent>
  ) {
	super(injector);
  }

  ngOnInit(): void {
	this.XXXEntityLowerSingularXXX.isActive = true;
  }

  save(): void {
	this.saving = true;

	const XXXEntityLowerSingularXXX_ = new XXXEntitySingularXXXCreateDto();
	XXXEntityLowerSingularXXX_.init(this.XXXEntityLowerSingularXXX);

	this._XXXEntityLowerSingularXXXService
	  .create(XXXEntityLowerSingularXXX_)
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
