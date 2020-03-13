import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { RolesComponent } from 'app/roles/roles.component';
import { CampaignTypesComponent } from 'app/campaigntypes/campaigntypes.component';
import { DetailCampaignComponent } from './campaigns/detail-campaign/detail-campaign.component';
import { CreateCampaignComponent } from './campaigns/create-campaign/create-campaign.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ApilogsComponent } from 'app/apilogs/apilogs.component';
import { CampaignactionsComponent } from 'app/campaignactions/campaignactions.component';
import { InstaaccountsComponent } from 'app/instaaccounts/instaaccounts.component';
import { DetailInstaaccountComponent } from './instaaccounts/detail-instaaccount/detail-instaaccount.component';
import { InfoInstaaccountComponent } from './instaaccounts/info-instaaccount/info-instaaccount.component';
import { BackgroundjobsComponent } from 'app/backgroundjobs/backgroundjobs.component';

///app-routing.module.ts.place1///
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent, canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'campaigns', component: CampaignsComponent, data: { permission: 'Pages.Campaigns' }, canActivate: [AppRouteGuard] },
                    { path: 'campaigns/retry/:retry', component: CampaignsComponent, data: { permission: 'Pages.Campaigns' }, canActivate: [AppRouteGuard] },
                    { path: 'campaigns/detail/:campaignId', component: DetailCampaignComponent, data: { permission: 'Pages.Campaigns' }, canActivate: [AppRouteGuard] },
                    { path: 'campaigns/create', component: CreateCampaignComponent, data: { permission: 'Pages.Campaigns' }, canActivate: [AppRouteGuard] },
                    { path: 'campaigns/create/:actionId', component: CreateCampaignComponent, data: { permission: 'Pages.Campaigns' }, canActivate: [AppRouteGuard] },
                    { path: 'campaignTypes', component: CampaignTypesComponent, data: { permission: 'Pages.CampaignTypes' }, canActivate: [AppRouteGuard] },
                    { path: 'update-password', component: ChangePasswordComponent },
                    { path: 'apilogs', component: ApilogsComponent, data: { permission: 'Pages.Apilogs' }, canActivate: [AppRouteGuard] },
                    { path: 'campaignactions', component: CampaignactionsComponent, data: { permission: 'Pages.Campaignactions' }, canActivate: [AppRouteGuard] },
                    { path: 'instaaccounts', component: InstaaccountsComponent, data: { permission: 'Pages.Instaaccounts' }, canActivate: [AppRouteGuard] },
                    { path: 'instaaccounts/detail/:instaAccountId', component: DetailInstaaccountComponent, data: { permission: 'Pages.Instaaccounts' }, canActivate: [AppRouteGuard] },
                    { path: 'instaaccounts/info/:instaAccountId', component: InfoInstaaccountComponent, data: { permission: 'Pages.Instaaccounts' }, canActivate: [AppRouteGuard] },
                    { path: 'backgroundjobs', component: BackgroundjobsComponent, data: { permission: 'Pages.Backgroundjobs' }, canActivate: [AppRouteGuard] },
                    ///app-routing.module.ts.place2///
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }

