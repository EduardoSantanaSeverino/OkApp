using System.Threading.Tasks;
using Abp.Application.Services;
using OkApp.Authorization.Accounts.Dto;

namespace OkApp.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

