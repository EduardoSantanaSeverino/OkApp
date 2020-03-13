import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from 'shared/paged-listing-component-base';
import { XXXEntitySingularXXXServiceProxy, XXXEntitySingularXXXDto, PagedResultDtoOfXXXEntitySingularXXXDto } from '@shared/service-proxies/service-proxies';
import { CreateXXXEntitySingularXXXDialogComponent } from './create-XXXEntityLowerSingularXXX/create-XXXEntityLowerSingularXXX-dialog.component';
import { EditXXXEntitySingularXXXDialogComponent } from './edit-XXXEntityLowerSingularXXX/edit-XXXEntityLowerSingularXXX-dialog.component';

class PagedModelsResultRequestDto extends PagedRequestDto {
	keyword: string;
	isActive: boolean | null;
}

@Component({
	templateUrl: './XXXEntityLowerPluralXXX.component.html',
	animations: [appModuleAnimation()],
	styles: [
		`
		  mat-form-field {
			padding: 10px;
		  }
		`
	]
})
export class XXXEntityPluralXXXComponent extends PagedListingComponentBase<XXXEntitySingularXXXDto> {
	XXXEntityLowerPluralXXX: XXXEntitySingularXXXDto[] = [];
	keyword = '';
	isActive: boolean | null;

	constructor(
		injector: Injector,
		private _XXXEntityLowerPluralXXXService: XXXEntitySingularXXXServiceProxy,
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

		this._XXXEntityLowerPluralXXXService
			.getAll(request.keyword, request.isActive, request.skipCount, request.maxResultCount)
			.pipe(
				finalize(() => {
					finishedCallback();
				})
			)
			.subscribe((result: PagedResultDtoOfXXXEntitySingularXXXDto) => {
				this.XXXEntityLowerPluralXXX = result.items;
				this.showPaging(result, pageNumber);
			});
	}

	protected delete(XXXEntityLowerSingularXXX: XXXEntitySingularXXXDto): void {
		abp.message.confirm(
			this.l('XXXEntitySingularXXXDeleteWarningMessage', XXXEntityLowerSingularXXX.name),
			(result: boolean) => {
				if (result) {
					this._XXXEntityLowerPluralXXXService
						.delete(XXXEntityLowerSingularXXX.id)
						.subscribe(() => { 
							abp.notify.success(this.l('SuccessfullyDeleted'));
							this.refresh();
					});
				}
			}
		);
	}

	createXXXEntitySingularXXX(): void {
		this.showCreateOrEditXXXEntitySingularXXXDialog();
	}

	editXXXEntitySingularXXX(XXXEntityLowerSingularXXX: XXXEntitySingularXXXDto): void {
		this.showCreateOrEditXXXEntitySingularXXXDialog(XXXEntityLowerSingularXXX.id);
	}

	private showCreateOrEditXXXEntitySingularXXXDialog(id?: number): void {
		let createOrEditXXXEntitySingularXXXDialog;
		if (id === undefined || id <= 0) {
			createOrEditXXXEntitySingularXXXDialog = this._dialog.open(CreateXXXEntitySingularXXXDialogComponent);
		} else {
			createOrEditXXXEntitySingularXXXDialog = this._dialog.open(EditXXXEntitySingularXXXDialogComponent, {
				data: id
			});
		}

		createOrEditXXXEntitySingularXXXDialog.afterClosed().subscribe(result => {
			if (result) {
				this.refresh();
			}
		});
	}
}
