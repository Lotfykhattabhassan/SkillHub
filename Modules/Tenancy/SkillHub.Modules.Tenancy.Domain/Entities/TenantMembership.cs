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

    private TenantMembership(Guid userId, int tenantId, MembershipRole role)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        if (tenantId <= 0)
            throw new ArgumentOutOfRangeException(nameof(tenantId));
        if (!Enum.IsDefined(role))
            throw new ArgumentOutOfRangeException(nameof(role));

        UserId = userId;
        TenantId = tenantId;
        Role = role;
        Status = MembershipStatus.Pending;
        JoinedAt = DateTime.UtcNow;
    }

    public static TenantMembership Create(Guid userId, int tenantId, MembershipRole role = MembershipRole.Member)
        => new(userId, tenantId, role);

    public void Accept()
    {
        if (Status == MembershipStatus.Pending || Status == MembershipStatus.Suspended)
            Status = MembershipStatus.Active;
    }

    public void ChangeRole(MembershipRole role)
    {
        if (!Enum.IsDefined(role))
            throw new ArgumentOutOfRangeException(nameof(role));
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
        if (Status != MembershipStatus.Revoked)
            Status = MembershipStatus.Revoked;
    }
}
