using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OkApp
{
    public interface IOkAppCrudAppServiceBase<TEntityDto, TPrimaryKey, in TRequestDto, in TCreateDto, in TUpdateDto> :
        IAsyncCrudAppService<TEntityDto, TPrimaryKey, TRequestDto, TCreateDto, TUpdateDto>
     where TEntityDto : IEntityDto<TPrimaryKey>
     where TUpdateDto : IEntityDto<TPrimaryKey>
     where TCreateDto : IEntityDto<TPrimaryKey>
    {
        Task<List<TEntityDto>> GetAllForCombo();
    }
}

