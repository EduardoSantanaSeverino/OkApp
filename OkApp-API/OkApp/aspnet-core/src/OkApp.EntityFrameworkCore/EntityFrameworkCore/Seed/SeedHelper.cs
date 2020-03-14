﻿using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using OkApp.EntityFrameworkCore.Seed.Host;
using OkApp.EntityFrameworkCore.Seed.Tenants;

namespace OkApp.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<OkAppDbContext>(iocResolver, SeedHostDb);
        }

        public static void SeedHostDb(OkAppDbContext context)
        {
            context.SuppressAutoSetTenantId = true;

            // Host seed
            new InitialHostDbBuilder(context).Create();

            // Default tenant seed (in host database).
            new DefaultTenantBuilder(context).Create();
            new TenantRoleAndUserBuilder(context, 1).Create();
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }

        public static T NewObje<T>(T obj)
        {

            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.Name == "CreationTime")
                {
                    prop.SetValue(obj, DateTime.Now);
                }
                if (prop.Name == "CreatorUserId")
                {
                    prop.SetValue(obj, 1L);
                }
                if (prop.Name == "IsActive")
                {
                    prop.SetValue(obj, true);
                }
            }

            return obj;
        }

    }
}

