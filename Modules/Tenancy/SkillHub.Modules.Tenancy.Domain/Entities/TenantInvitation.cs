using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.BuildingBlocks.Domain.Common;
using SkillHub.BuildingBlocks.Domain.Common.Events;
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
        if ( string.IsNullOrWhiteSpace( email ))
            throw new ArgumentNullException(nameof(email));
        if ( !email.Contains("@") )
            throw new ArgumentException("Email must contains @",nameof(email));
        Email = email;
        Status = InvitationStatus.Pending;
        InvitedAt = DateTime.UtcNow;
        if ( expiresAt <= DateTime.UtcNow )
            throw new ArgumentOutOfRangeException(nameof(expiresAt));
        ExpiresAt = expiresAt;
    }
    public void Accept()
    {
        if (Status != InvitationStatus.Pending)
            return;

        if (DateTime.UtcNow >= ExpiresAt)
        {
            Status = InvitationStatus.Expired;
            return;
        }

        Status = InvitationStatus.Accepted;

        AddDomainEvent(
            new InvitationAccepted(Id, TenantId));
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
    public void Expire()
    {
        if (Status == InvitationStatus.Pending &&
            DateTime.UtcNow >= ExpiresAt)
        {
            Status = InvitationStatus.Expired;
        }
    }
}
