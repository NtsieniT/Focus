using Focus.Incident.Domain.Incident.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Incident.Common.Model
{
    public abstract class EntityBaseWithTypedId<TId> : ValidatableObject, IEntityWithTypedId<TId>
    {
        public virtual TId Id { get; set; }
    }
}
