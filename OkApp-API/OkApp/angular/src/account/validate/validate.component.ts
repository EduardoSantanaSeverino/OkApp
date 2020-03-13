import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import {
	InstaAccountDto, InstaAccountServiceProxy, ChallengeResult
} from '@shared/service-proxies/service-proxies';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AppAuthService } from '@shared/auth/app-auth.service';
import { AppConsts } from '@shared/AppConsts';
import { UpdatePasswordDialogComponent } from './update-password/update-password-dialog.component';

enum AccountStateEnum {
	Verified = 1,
	BadPassword,
	InvalidUser,
	TwoFactorRequired,
	Exception,
	ChallengeRequired,
	LimitError,
	InactiveUser,
	CheckpointLoggedOut
}

@Component({
	templateUrl: './validate.component.html',
	animations: [accountModuleAnimation()],
	styles: [
		`
	mat-form-field {
		width: 100%;
	}
	mat-checkbox {
		padding-bottom: 5px;
	}

	.margin-left button
	{
		margin-left: 10px;
	}
	.mat-radio-button ~ .mat-radio-button {
		margin-left: 16px;
	}
	.margin-right { margin-right: 10px; }

	`
	]
})
export class ValidateComponent extends AppComponentBase implements OnInit {

	Instaaccount: InstaAccountDto = new InstaAccountDto();
	InstaaccountId: number = 0;
	twoFactorForm: FormGroup;
	challengeRequiredMethodsForm: FormGroup;
	challengeRequiredPhoneForm: FormGroup;
	challengeRequiredVerifyForm: FormGroup;
	twoFactor: boolean = false;
	phoneNumberDesc: string = "Phone Number";
	emailDesc: string = "Email";
	showEmail: boolean = false;
	showPhoneNumber: boolean = false;
	twoFactorResult: string = "Well Done";
	challengeRequiredResult: string = "Well Done";
	twoFactorCode: string = "";
	challengeRequired: boolean = false;
	sendVia?: number;
	phoneRequired: boolean = false;
	phoneNumber: string = "";
	verifyCode: string = "";

	constructor(
		injector: Injector,
		private _Instaaccountservice: InstaAccountServiceProxy,
		private _formBuilder: FormBuilder,
		private _activatedRoute: ActivatedRoute,
		private _dialog: MatDialog,
		private _authService: AppAuthService
	) {
		super(injector);
	}

	ngOnInit(): void {
		this._activatedRoute.params.subscribe((params: Params) => {
			$('body').addClass('login-page-custom-size');
			this.InstaaccountId = params['instaAccountId'];
			this.loadInstaAccount();
			this.loadInitialForm();
		});
	}

	loadInstaAccount(): void {
		this._Instaaccountservice.get(this.InstaaccountId)
			.subscribe((result: InstaAccountDto) => {
				this.Instaaccount = result;
				this.loadInstaAccountControls();
			});
	}

	loadInstaAccountControls() {
		if (this.Instaaccount.accountStateId == AccountStateEnum.Verified) {
			location.href = AppConsts.appBaseUrl;
		}
		this.showEmail = false;
		this.showPhoneNumber = false;
		this.challengeRequired = (this.Instaaccount.accountStateId == AccountStateEnum.ChallengeRequired);
		this.twoFactor = (this.Instaaccount.accountStateId == AccountStateEnum.TwoFactorRequired);
		if (this.challengeRequired) {
			this._Instaaccountservice
				.getChallengeRequireVerifyMethodAsync(this.InstaaccountId)
				.subscribe((resultChallenge: ChallengeResult) => {

					if (resultChallenge.succeeded) {
						if (resultChallenge.submitPhoneRequired) {
							this.phoneRequired = true;
						}
						else {

							if (resultChallenge.phoneNumber) {
								this.emailDesc = "Phone Number " + resultChallenge.phoneNumber;
								this.showPhoneNumber = true;
							}

							if (resultChallenge.email) {
								this.emailDesc = "Email " + resultChallenge.email;
								this.showEmail = true;
							}

						}
					}

				});

		}
	}

	loadInitialForm(): void {
		this.twoFactorForm = this._formBuilder.group({
			twoFactorCode: ['', Validators.required]
		});
		this.challengeRequiredMethodsForm = this._formBuilder.group({
			sendVia: ['', Validators.required]
		});
		this.challengeRequiredPhoneForm = this._formBuilder.group({
			phoneNumber: ['', Validators.required]
		});
		this.challengeRequiredVerifyForm = this._formBuilder.group({
			verifyCode: ['', Validators.required]
		});

	}

	twoFactorSubmit(): void {
		this.twoFactorResult = "";
		this._Instaaccountservice
			.twoFactorLoginAsync(this.InstaaccountId, this.twoFactorCode)
			.subscribe((result: ChallengeResult) => {
				if (result.succeeded) {
					this.twoFactorResult = "Succeeded " + result.message;
					this.notify.info(this.twoFactorResult);
				}
				else {
					this.twoFactorResult = result.message;
					this.notify.error(this.twoFactorResult);
				}

			});
	}

	challengeRequiredMethodsSubmit(): void {
		this.challengeRequiredResult = "";
		this._Instaaccountservice
			.requestVerifyCodeForChallenge(this.InstaaccountId, this.sendVia)
			.subscribe((result: ChallengeResult) => {
				if (result.succeeded) {
					this.challengeRequiredResult = "Succeeded " + result.message;
					this.notify.info(this.challengeRequiredResult);
				}
				else {
					this.challengeRequiredResult = result.message;
					this.notify.error(this.challengeRequiredResult);
				}
			});
	}

	challengeRequiredPhoneSubmit(): void {
		this._Instaaccountservice
			.submitPhoneNumberForChallengeRequireAsync(this.InstaaccountId, this.phoneNumber)
			.subscribe((result: ChallengeResult) => {
				this.challengeRequiredResult = "";
				if (result.succeeded) {
					this.challengeRequiredResult = "Succeeded " + result.message;
					this.notify.info(this.challengeRequiredResult);
				}
				else {
					this.challengeRequiredResult = result.message;
					this.notify.error(this.challengeRequiredResult);
				}

			});
	}

	challengeRequiredVerifySubmit(): void {
		this.challengeRequiredResult = "";
		this._Instaaccountservice
			.verifyCodeForChallengeRequireAsync(this.InstaaccountId, this.verifyCode, this.sendVia)
			.subscribe((result: ChallengeResult) => {
				if (result.succeeded) {
					this.challengeRequiredResult = "Succeeded " + result.message;
					this.notify.info(this.challengeRequiredResult);
				}
				else {
					this.challengeRequiredResult = result.message;
					this.notify.error(this.challengeRequiredResult);
				}

			});
	}

	resendVerificationCode(): void {
		this.challengeRequiredResult = "";
		this._Instaaccountservice
			.requestVerifyCodeForChallengeResend(this.InstaaccountId, this.sendVia)
			.subscribe((result: ChallengeResult) => {

				if (result.succeeded) {
					this.challengeRequiredResult = "Succeeded " + result.message;
					this.notify.info(this.challengeRequiredResult);
				}
				else {
					this.challengeRequiredResult = result.message;
					this.notify.error(this.challengeRequiredResult);
				}

			});
	}

	checkUserStatus(): void {
		this._Instaaccountservice
			.checkStatus(this.InstaaccountId)
			.subscribe((result: InstaAccountDto) => {
				this.Instaaccount = result;
				debugger;
				this.loadInstaAccountControls();
			});
	}

	editInstaaccount(instaAccount: InstaAccountDto): void {
		debugger;
		this.showCreateOrEditInstaaccountDialog(instaAccount.id);
	}

	private showCreateOrEditInstaaccountDialog(id?: number): void {
		let editInstaAccountDialog;

		editInstaAccountDialog = this._dialog.open(UpdatePasswordDialogComponent, {
			data: { id: id, onlyUpdatePassword: true }
		});

		editInstaAccountDialog.afterClosed().subscribe(result => {
			if (result) {
				this.checkUserStatus();
			}
		});
	}

	logout(): void {
		this._authService.logout();
	}

}
