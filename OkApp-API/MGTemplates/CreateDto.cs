using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using XXXProjectNameXXX.Models;

namespace XXXProjectNameXXX.XXXEntityPluralXXX.Dto
{
    [AutoMapTo(typeof(XXXEntitySingularXXX))]
    public class XXXEntitySingularXXXCreateDto : EntityDto<XXXSpecificTypeXXX>
    {

        ///CreateDto.cs.fields1///

    }
}
