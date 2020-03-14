using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using OkApp.Authorization.Roles;
using OkApp.Authorization.Users;
using OkApp.MultiTenancy;

namespace OkApp.EntityFrameworkCore
{
    public class OkAppDbContext : AbpZeroDbContext<Tenant, Role, User, OkAppDbContext>
    {
        public OkAppDbContext(DbContextOptions<OkAppDbContext> options)
            : base(options)
        {
        }
    }
}

