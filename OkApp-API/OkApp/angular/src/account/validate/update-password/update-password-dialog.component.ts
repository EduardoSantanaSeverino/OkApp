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
    InstaAccountServiceProxy,
    InstaAccountDto
} from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: 'update-password-dialog.component.html',
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
export class UpdatePasswordDialogComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    instaAccount: InstaAccountDto = new InstaAccountDto();
    onlyUpdatePassword: boolean = false;

    constructor(
        injector: Injector,
        private _instaAccountService: InstaAccountServiceProxy,
        private _dialogRef: MatDialogRef<UpdatePasswordDialogComponent>,
        @Optional() @Inject(MAT_DIALOG_DATA) private data: any
    ) {
        super(injector);
    }

    ngOnInit(): void {

        if (this.data.onlyUpdatePassword)
        {
            this.onlyUpdatePassword = this.data.onlyUpdatePassword;
        }
        this._instaAccountService
            .get(this.data.id)
            .subscribe(result => {
                this.instaAccount = result;
            });
    }

    save(): void {
        this.saving = true;

        this._instaAccountService
            .update(this.instaAccount)
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

