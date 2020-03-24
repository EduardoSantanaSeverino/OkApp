using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Localization;
using OkApp.Languages.Dto;

namespace OkApp.Languages
{
    [AbpAuthorize]
    public class LanguageAppService : ApplicationService, ILanguageAppService
    {
        private readonly IApplicationLanguageManager _applicationLanguageManager;

        public LanguageAppService
        (
            IApplicationLanguageManager applicationLanguageManager
        )
        {
            _applicationLanguageManager = applicationLanguageManager;
        }

        public async Task<List<LanguageDto>> GetLanguages()
        {
            var query = await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId);
            var output = ObjectMapper.Map<List<LanguageDto>>(query.ToList());
            return output;
        }

        public async Task Add(LanguageCreateDto language)
        {
            var output = ObjectMapper.Map<ApplicationLanguage>(language);
            await _applicationLanguageManager.AddAsync(output);
        }

        public async Task Remove(string languageName)
        {
            await _applicationLanguageManager.RemoveAsync(AbpSession.TenantId, languageName);
        }

        public async Task Update(LanguageUpdateDto language)
        {
            var output = ObjectMapper.Map<ApplicationLanguage>(language);
            await _applicationLanguageManager.UpdateAsync(AbpSession.TenantId, output);
        }

        public async Task<LanguageDto> GetDefaultLanguageOrNull()
        {
            var obj = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
            var output = ObjectMapper.Map<LanguageDto>(obj);
            return output;
        }

        public async Task SetDefaultLanguage(string languageName)
        {
            await _applicationLanguageManager.SetDefaultLanguageAsync(AbpSession.TenantId, languageName);
        }
    }
}