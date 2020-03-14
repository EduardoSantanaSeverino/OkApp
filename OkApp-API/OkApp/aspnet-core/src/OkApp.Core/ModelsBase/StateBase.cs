using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace OkApp.ModelsBase
{
    public abstract class StateBase<TKeyType> : FullAuditedEntity<TKeyType>, IPassivable
    {
        public StateBase()
        {
            this.IsActive = true;
            this.CreationTime = DateTime.Now;
        }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}

