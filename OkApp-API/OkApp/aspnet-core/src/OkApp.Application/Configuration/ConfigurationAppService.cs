using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using OkApp.Configuration.Dto;

namespace OkApp.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : OkAppAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

