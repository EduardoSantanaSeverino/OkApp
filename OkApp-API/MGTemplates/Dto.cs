using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using XXXProjectNameXXX.Authorization.Users;
using XXXProjectNameXXX.Models;

namespace XXXProjectNameXXX.XXXEntityPluralXXX.Dto
{
    [AutoMapFrom(typeof(XXXEntitySingularXXX))]
    public class XXXEntitySingularXXXDto : EntityDto<XXXSpecificTypeXXX>
    {

        ///Dto.cs.fields1///

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        public long? CreatorUserId { get; set; }

    }
}
