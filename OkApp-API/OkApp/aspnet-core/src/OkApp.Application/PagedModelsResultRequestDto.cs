using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace OkApp
{
    // Custom all models PagedResultRequestDto
    public class PagedModelsResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}

