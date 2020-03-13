using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using OkApp.Authorization.Roles;
using OkApp.MultiTenancy;

namespace OkApp.Authorization.Users
{
    public class UserRegistrationManager : DomainService
    {
        public IAbpSession AbpSession { get; set; }

        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRegistrationManager(
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager,
            IPasswordHasher<User> passwordHasher)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            AbpSession = NullAbpSession.Instance;
        }

        public async Task<User> RegisterAsync(string name, string surname, string emailAddress, string userName, string plainPassword, bool isEmailConfirmed)
        {
            CheckForTenant();

            var tenant = await GetActiveTenantAsync();

            var user = new User
            {
                TenantId = tenant.Id,
                Name = name,
                Surname = surname,
                EmailAddress = emailAddress,
                IsActive = true,
                UserName = userName,
                IsEmailConfirmed = isEmailConfirmed,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();
           
            foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
            {
                user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRole.Id));
            }

            await _userManager.InitializeOptionsAsync(tenant.Id);

            CheckErrors(await _userManager.CreateAsync(user, plainPassword));
            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<UserTenantModel> RegisterUserAndTenantAsync(string name, string surname, string emailAddress, string userName, string plainPassword, bool isEmailConfirmed)
        {
           
            var retVal = new UserTenantModel();
            
            var user = new User
            {
                Name = name,
                Surname = surname,
                EmailAddress = emailAddress,
                IsActive = true,
                UserName = userName,
                IsEmailConfirmed = isEmailConfirmed,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();
           
            var tenant = await CreateTenant(new Tenant
            {
                ConnectionString = null,
                IsActive = true,
                Name = GetTenantName(user.Name, user.Surname),
                TenancyName = GetTenantName(user.Name, user.Surname)
            }, user
            , plainPassword);

            retVal.User = user;
            retVal.Tenant = tenant;

            return retVal;
        }

        private async Task<Tenant> CreateTenant(Tenant tenant, User user, string plainPassword)
        {
            var defaultEdition = new { Id = 1 };
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            await _tenantManager.CreateAsync(tenant);
            await CurrentUnitOfWork.SaveChangesAsync(); // To get new tenant's id.


            // We are working entities of new tenant, so changing tenant filter
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                // Create static roles for new tenant
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); // To get static role ids

                // Grant all permissions to admin role
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                // Create admin user for the tenant
                user.TenantId = tenant.Id;
                await _userManager.InitializeOptionsAsync(tenant.Id);
                CheckErrors(await _userManager.CreateAsync(user, plainPassword));
                await CurrentUnitOfWork.SaveChangesAsync(); // To get admin user's id


                // Assign admin user to role!
                CheckErrors(await _userManager.AddToRoleAsync(user, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return tenant;
        }

        private string GetTenantName(string name, string surname)
        {
            int nextId = _tenantManager.Tenants.Max(p => p.Id) + 1;
            return this.Capitalize(name) + this.Capitalize(surname) + $"{nextId}";
        }

        private string Capitalize(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return "";
            }
            else
            {
                word = word.Trim();
                word = word.Replace(" ", "");
            }
            return word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
        }

        private void CheckForTenant()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                throw new InvalidOperationException("Can not register host users!");
            }
        }

        private async Task<Tenant> GetActiveTenantAsync()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return await GetActiveTenantAsync(AbpSession.TenantId.Value);
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await _tenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }

            return tenant;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

