using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using XXXProjectNameXXX.XXXEntityPluralXXX.Dto;
using XXXProjectNameXXX.Models;

namespace XXXProjectNameXXX.XXXEntityPluralXXX
{

    [AbpAuthorize]
    public class XXXEntitySingularXXXAppService : XXXProjectNameXXXCrudAppServiceBase<XXXEntitySingularXXX, XXXEntitySingularXXXDto, XXXSpecificTypeXXX, PagedModelsResultRequestDto, XXXEntitySingularXXXCreateDto, XXXEntitySingularXXXUpdateDto>, IXXXEntitySingularXXXAppService
    {
        public XXXEntitySingularXXXAppService
        (
            IRepository<XXXEntitySingularXXX, XXXSpecificTypeXXX> repository
        )
        : base(repository)
        {
           
        }

    }
}

