import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JsonpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AbpModule } from '@abp/abp.module';

import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';

import { HomeComponent } from '@app/home/home.component';
import { AboutComponent } from '@app/about/about.component';
import { TopBarComponent } from '@app/layout/topbar.component';
import { TopBarLanguageSwitchComponent } from '@app/layout/topbar-languageswitch.component';
import { SideBarUserAreaComponent } from '@app/layout/sidebar-user-area.component';
import { SideBarNavComponent } from '@app/layout/sidebar-nav.component';
import { SideBarFooterComponent } from '@app/layout/sidebar-footer.component';
import { RightSideBarComponent } from '@app/layout/right-sidebar.component';
// tenants
import { TenantsComponent } from '@app/tenants/tenants.component';
import { CreateTenantDialogComponent } from './tenants/create-tenant/create-tenant-dialog.component';
import { EditTenantDialogComponent } from './tenants/edit-tenant/edit-tenant-dialog.component';
// roles
import { RolesComponent } from '@app/roles/roles.component';
import { CreateRoleDialogComponent } from './roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './roles/edit-role/edit-role-dialog.component';
// users
import { UsersComponent } from '@app/users/users.component';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from './users/reset-password/reset-password.component';
// campaignTypes
import { CampaignTypesComponent } from '@app/campaignTypes/campaignTypes.component';
import { CreateCampaignTypeDialogComponent } from '@app/campaignTypes/create-CampaignType/create-campaignType-dialog.component';
import { EditCampaignTypeDialogComponent } from '@app/campaignTypes/edit-CampaignType/edit-CampaignType-dialog.component';
// campaigns
import { CampaignsComponent } from '@app/campaigns/campaigns.component';
import { CreateCampaignDialogComponent } from './campaigns/create-campaign/create-campaign-dialog.component';
import { EditCampaignDialogComponent } from './campaigns/edit-campaign/edit-campaign-dialog.component';
///app.module.ts.place1///
@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        AboutComponent,
        TopBarComponent,
        TopBarLanguageSwitchComponent,
        SideBarUserAreaComponent,
        SideBarNavComponent,
        SideBarFooterComponent,
        RightSideBarComponent,
        // tenants
        TenantsComponent,
        CreateTenantDialogComponent,
        EditTenantDialogComponent,
        // roles
        RolesComponent,
        CreateRoleDialogComponent,
        EditRoleDialogComponent,
        // users
        UsersComponent,
        CreateUserDialogComponent,
        EditUserDialogComponent,
        ChangePasswordComponent,
        ResetPasswordDialogComponent,
        // campaignTypes
        CampaignTypesComponent,
        CreateCampaignTypeDialogComponent,
        EditCampaignTypeDialogComponent,
        // roles
        CampaignsComponent,
        CreateCampaignDialogComponent,
        EditCampaignDialogComponent,
        ///app.module.ts.place2///
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        JsonpModule,
        ModalModule.forRoot(),
        AbpModule,
        AppRoutingModule,
        ServiceProxyModule,
        SharedModule,
        NgxPaginationModule
    ],
    providers: [],
    entryComponents: [
        // tenants
        CreateTenantDialogComponent,
        EditTenantDialogComponent,
        // roles
        CreateRoleDialogComponent,
        EditRoleDialogComponent,
        // users
        CreateUserDialogComponent,
        EditUserDialogComponent,
        ResetPasswordDialogComponent,
        // campaignTypes
        CreateCampaignTypeDialogComponent,
        EditCampaignTypeDialogComponent,
        // campaign
        CreateCampaignDialogComponent,
        EditCampaignDialogComponent,
        ///app.module.ts.place3///
    ]
})
export class AppModule { }
