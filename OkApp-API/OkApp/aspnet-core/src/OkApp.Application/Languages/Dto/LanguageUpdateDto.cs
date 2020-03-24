using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Localization;
using Abp.Runtime.Validation;

namespace OkApp.Languages.Dto
{
    [AutoMapTo(typeof(ApplicationLanguage))]
    public class LanguageUpdateDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public bool IsDisabled { get; set; }
        ///UpdateDto.cs.fields1///
    }
}