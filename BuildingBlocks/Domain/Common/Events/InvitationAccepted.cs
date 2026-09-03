using System;
using System.Collections.Generic;
using System.Text;

namespace SkillHub.BuildingBlocks.Domain.Common.Events;

public class InvitationAccepted : DomainEvent
{
    public int InvitationId { get; }
    public int TenantId { get; }

    public InvitationAccepted(int invitationId, int tenantId)
    {
        InvitationId = invitationId;
        TenantId = tenantId;
    }
}
