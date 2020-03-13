using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Runtime.Security;
using Abp.Threading.BackgroundWorkers;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using OkApp.Authorization.Accounts.Dto;
using OkApp.Authorization.Roles;
using OkApp.Authorization.Users;

using OkApp.MultiTenancy;
using OkApp.MultiTenancy.Dto;

namespace OkApp.Authorization.Accounts
{
    public class AccountAppService : OkAppAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IBackgroundWorkerManager _workManager;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager,
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager,
            IBackgroundWorkerManager workManager)
        {
            _userRegistrationManager = userRegistrationManager;
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _workManager = workManager;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var model = await _userRegistrationManager.RegisterUserAndTenantAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = model.User.IsActive && (model.User.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin),
                TenancyName = model.Tenant.TenancyName
            };

        }
    }
}

