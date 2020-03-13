using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SocialUplift.Authorization
{
    public class SocialUpliftAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Campaigns, L("Campaigns"));
            context.CreatePermission(PermissionNames.Pages_CampaignStates, L("CampaignStates"));
            context.CreatePermission(PermissionNames.Pages_CampaignTypes, L("CampaignTypes"));
            context.CreatePermission(PermissionNames.Pages_InstaAccounts, L("InstaAccounts"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            ///AuthorizationProvider.cs.place1///
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SocialUpliftConsts.LocalizationSourceName);
        }
    }
}
