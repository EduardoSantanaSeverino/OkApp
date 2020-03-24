import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AbpHttpInterceptor } from '@abp/abpHttpInterceptor';

import * as ApiServiceProxies from './service-proxies';

@NgModule({
    providers: [
        ApiServiceProxies.RoleServiceProxy,
        ApiServiceProxies.SessionServiceProxy,
        ApiServiceProxies.TenantServiceProxy,
        ApiServiceProxies.UserServiceProxy,
        ApiServiceProxies.TokenAuthServiceProxy,
        ApiServiceProxies.AccountServiceProxy,
        ApiServiceProxies.CampaignServiceProxy,
        ApiServiceProxies.CampaignStateServiceProxy,
        ApiServiceProxies.CampaignTypeServiceProxy,
		ApiServiceProxies.InstaAccountServiceProxy,
		ApiServiceProxies.ApilogServiceProxy,
        ApiServiceProxies.CampaignactionServiceProxy,
        ApiServiceProxies.BackgroundjobServiceProxy,
        ApiServiceProxies.LanguageServiceProxy,
///service-proxy.module.ts.place1///
        { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true }
    ]
})
export class ServiceProxyModule { }




