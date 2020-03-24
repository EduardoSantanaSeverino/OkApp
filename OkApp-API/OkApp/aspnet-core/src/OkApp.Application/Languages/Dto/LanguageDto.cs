using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Localization;
using OkApp.Authorization.Users;

namespace OkApp.Languages.Dto
{
    [AutoMapFrom(typeof(ApplicationLanguage))]
    public class LanguageDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public bool IsDisabled { get; set; }
        public int? TenantId { get; set; }

        ///Dto.cs.fields1///

        public DateTime CreationTime { get; set; }

        public long? CreatorUserId { get; set; }
    }
}