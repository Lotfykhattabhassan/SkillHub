using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.BuildingBlocks.Domain.Common.Events;

public interface IDomainEvent
{
    public DateTime OccurredOn { get; }
}
