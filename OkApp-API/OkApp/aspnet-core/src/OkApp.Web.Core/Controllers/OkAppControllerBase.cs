using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace OkApp.Controllers
{
    public abstract class OkAppControllerBase: AbpController
    {
        protected OkAppControllerBase()
        {
            LocalizationSourceName = OkAppConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

