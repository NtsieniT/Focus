using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Incident.Domain.Incident.Common.Interface
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}
