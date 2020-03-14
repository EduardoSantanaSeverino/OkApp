using Abp.Authorization;
using OkApp.Authorization.Roles;
using OkApp.Authorization.Users;

namespace OkApp.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

