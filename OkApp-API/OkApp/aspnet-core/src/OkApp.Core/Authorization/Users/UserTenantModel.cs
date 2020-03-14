
using OkApp.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace OkApp.Authorization.Users
{
    public class UserTenantModel
    {
        public User User { get; set; }
        public Tenant Tenant { get; set; }
    }
}

