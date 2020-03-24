import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
    templateUrl: './sidebar-nav.component.html',
    selector: 'sidebar-nav',
    encapsulation: ViewEncapsulation.None
})
export class SideBarNavComponent extends AppComponentBase {

    menuItems: MenuItem[] = [
        new MenuItem(this.l('HomePage'), '', 'home', '/app/home'),
        //new MenuItem(this.l('Tenants'), 'Pages.Tenants', 'business', '/app/tenants'),
        new MenuItem(this.l('Users'), 'Pages.Users', 'people', '/app/users'),
        //new MenuItem(this.l('Roles'), 'Pages.Roles', 'local_offer', '/app/roles'),
        new MenuItem(this.l('Campaigns'), 'Pages.Campaigns', 'line_style', '/app/campaigns'),
        //new MenuItem(this.l('CampaignTypes'), 'Pages.CampaignTypes', 'local_offer', '/app/campaignTypes'),
        new MenuItem(this.l('Apilogs'), 'Pages.Apilogs', 'track_changes', '/app/apilogs'),
        new MenuItem(this.l('Accounts'), 'Pages.Instaaccounts', 'account_box', '/app/instaaccounts'),
        new MenuItem(this.l('Backgroundjobs'), 'Pages.Backgroundjobs', 'local_offer', '/app/backgroundjobs'),
        new MenuItem(this.l('Languages'), 'Pages.Languages', 'local_offer', '/app/languages'),
///sidebar-nav.component.ts.place1///
    ];

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }
}





