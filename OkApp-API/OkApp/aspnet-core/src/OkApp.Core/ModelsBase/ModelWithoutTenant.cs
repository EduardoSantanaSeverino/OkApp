using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace OkApp.ModelsBase
{
    public abstract class ModelWithoutTenant<TKeyType> : FullAuditedEntity<TKeyType>, IPassivable
    {
        public ModelWithoutTenant()
        {
            this.IsActive = true;
        }
        public bool IsActive { get; set; }

    }
}

