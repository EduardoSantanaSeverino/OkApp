using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace OkApp.ModelsBase
{
    public abstract class  ModelWithTenant<TKeyType> : FullAuditedEntity<TKeyType>, IPassivable, IMustHaveTenant
    {
        public ModelWithTenant()
        {
            this.IsActive = true;
            this.CreationTime = DateTime.Now;
        }
        public bool IsActive { get; set; }

        public int TenantId { get; set; }

    }
}

