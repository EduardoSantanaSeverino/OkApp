import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from 'shared/paged-listing-component-base';
import { LanguageServiceProxy, LanguageDto, PagedResultDtoOfLanguageDto } from '@shared/service-proxies/service-proxies';
import { CreateLanguageDialogComponent } from './create-language/create-language-dialog.component';
import { EditLanguageDialogComponent } from './edit-language/edit-language-dialog.component';

class PagedModelsResultRequestDto extends PagedRequestDto {
	keyword: string;
	isActive: boolean | null;
}

@Component({
	templateUrl: './languages.component.html',
	animations: [appModuleAnimation()],
	styles: [
		`
		  mat-form-field {
			padding: 10px;
		  }
		`
	]
})
export class LanguagesComponent extends PagedListingComponentBase<LanguageDto> {
	languages: LanguageDto[] = [];
	keyword = '';
	isActive: boolean | null;

	constructor(
		injector: Injector,
		private _languagesService: LanguageServiceProxy,
		private _dialog: MatDialog
	) {
		super(injector);
	}

	protected list(
		request: PagedModelsResultRequestDto,
		pageNumber: number,
		finishedCallback: Function
	): void {

		request.keyword = this.keyword;
		request.isActive = this.isActive;

		this._languagesService
			.getAll(request.keyword, request.isActive, request.skipCount, request.maxResultCount)
			.pipe(
				finalize(() => {
					finishedCallback();
				})
			)
			.subscribe((result: PagedResultDtoOfLanguageDto) => {
				this.languages = result.items;
				this.showPaging(result, pageNumber);
			});
	}

	protected delete(language: LanguageDto): void {
		abp.message.confirm(
			this.l('LanguageDeleteWarningMessage', language.name),
			(result: boolean) => {
				if (result) {
					this._languagesService
						.delete(language.id)
						.subscribe(() => { 
							abp.notify.success(this.l('SuccessfullyDeleted'));
							this.refresh();
					});
				}
			}
		);
	}

	createLanguage(): void {
		this.showCreateOrEditLanguageDialog();
	}

	editLanguage(language: LanguageDto): void {
		this.showCreateOrEditLanguageDialog(language.id);
	}

	private showCreateOrEditLanguageDialog(id?: number): void {
		let createOrEditLanguageDialog;
		if (id === undefined || id <= 0) {
			createOrEditLanguageDialog = this._dialog.open(CreateLanguageDialogComponent);
		} else {
			createOrEditLanguageDialog = this._dialog.open(EditLanguageDialogComponent, {
				data: id
			});
		}

		createOrEditLanguageDialog.afterClosed().subscribe(result => {
			if (result) {
				this.refresh();
			}
		});
	}
}

