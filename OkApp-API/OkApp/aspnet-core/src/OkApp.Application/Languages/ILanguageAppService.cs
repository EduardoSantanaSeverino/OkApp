using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using OkApp.Languages.Dto;

namespace OkApp.Languages
{
    public interface ILanguageAppService 
    {
    
        /// <summary>
        /// Gets list of all languages available to given tenant (or null for host)
        /// </summary>
        Task<List<LanguageDto>> GetLanguages();

        /// <summary>
        /// Adds a new language.
        /// </summary>
        /// <param name="language">The language.</param>
        Task Add(LanguageCreateDto language);

        /// <summary>
        /// Deletes a language.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        Task Remove(string languageName);

        /// <summary>
        /// Updates a language.
        /// </summary>
        /// <param name="language">The language to be updated</param>
        Task Update(LanguageUpdateDto language);

        /// <summary>
        /// Gets the default language or null for a tenant or the host.
        /// </summary>
        Task<LanguageDto> GetDefaultLanguageOrNull();

        /// <summary>
        /// Sets the default language for a tenant or the host.
        /// </summary>
        /// <param name="languageName">Name of the language.</param>
        Task SetDefaultLanguage(string languageName);
        
    }
}

