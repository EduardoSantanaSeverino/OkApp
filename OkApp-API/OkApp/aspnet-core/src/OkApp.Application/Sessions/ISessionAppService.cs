using System.Threading.Tasks;
using Abp.Application.Services;
using OkApp.Sessions.Dto;

namespace OkApp.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

