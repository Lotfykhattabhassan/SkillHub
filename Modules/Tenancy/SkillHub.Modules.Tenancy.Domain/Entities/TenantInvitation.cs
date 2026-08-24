using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.BuildingBlocks.Domain.Common;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Domain.Entities;

public class TenantInvitation : BaseEntity<int>
{
    public int TenantId { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public MembershipRole Role { get; private set; }
    public InvitationStatus Status { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime InvitedAt { get; private set; }

    private TenantInvitation() { }
    public TenantInvitation(int id, int tenantId,string email, MembershipRole role, DateTime expiresAt) 
        : base(id)
    {
        if ( tenantId <= 0 ) throw new ArgumentOutOfRangeException(nameof(tenantId));

        TenantId = tenantId;
        Role = role;
        Email = email;
        Status = InvitationStatus.Pending;
        InvitedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
    }

    public record InvitationAccepted(int id, int tenantId)
    {
        public DateTime OccurredAt { get; } = DateTime.UtcNow;
    }

    public void Accept()
    {
        if (Status == InvitationStatus.Pending || Status == InvitationStatus.Expired )
            Status = InvitationStatus.Accepted;
    }

    public void Revoke()
    {
        if (Status == InvitationStatus.Pending)
            Status = InvitationStatus.Revoked;
    }
    public void Reject()
    {
        if (Status == InvitationStatus.Pending)
            Status = InvitationStatus.Rejected  ;
    }
}
