﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using OkApp.MultiTenancy.Dto;

namespace OkApp.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

