using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.BuildingBlocks.Domain.Common;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Domain.Entities;

public class TenantMembership : BaseEntity<int>
{
    public Guid UserId { get; private set; }
    public int TenantId { get; private set; }
    public MembershipRole Role { get; private set; }
    public MembershipStatus Status { get; private set; }
    public DateTime JoinedAt { get; private set; }

    private TenantMembership() { }

    public TenantMembership(int id, Guid userId, int tenantId)
        : base(id)
    {
        UserId = userId;
        TenantId = tenantId;
        Role = MembershipRole.Member;
        Status = MembershipStatus.Pending;
        JoinedAt = DateTime.UtcNow;
    }

    public void Accept()
    {
        if (Status == MembershipStatus.Pending ||
            Status == MembershipStatus.Suspended)
                Status = MembershipStatus.Active;
        
    }
    public void ChangeRole(MembershipRole role)
    {
        if (Role == role) return;

        Role = role;
    }

    public void Suspend()
    {
        if (Status == MembershipStatus.Active)
            Status = MembershipStatus.Suspended;
    }

    public void Revoke()
    {
        if (Status == MembershipStatus.Revoked)
            return;

        Status = MembershipStatus.Revoked;
    }
}
