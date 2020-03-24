import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
  LanguageServiceProxy,
  LanguageDto,
  LanguageCreateDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-language-dialog.component.html',
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
export class CreateLanguageDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  language: LanguageDto = new LanguageDto();

  constructor(
	injector: Injector,
	private _languageService: LanguageServiceProxy,
	private _dialogRef: MatDialogRef<CreateLanguageDialogComponent>
  ) {
	super(injector);
  }

  ngOnInit(): void {
	this.language.isActive = true;
  }

  save(): void {
	this.saving = true;

	const language_ = new LanguageCreateDto();
	language_.init(this.language);

	this._languageService
	  .create(language_)
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

