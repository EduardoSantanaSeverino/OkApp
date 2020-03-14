using System.Threading.Tasks;
using OkApp.Configuration.Dto;

namespace OkApp.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

