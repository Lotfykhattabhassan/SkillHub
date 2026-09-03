using System;
using System.Collections.Generic;
using System.Text;
using SkillHub.BuildingBlocks.Domain.Common;
using SkillHub.Modules.Identity.Domain.Enums;

namespace SkillHub.Modules.Identity.Domain.Entities;

public class UserPlatformRole : BaseEntity<int>
{
    public Guid UserId { get; private set; }
    public PlatformRole Role { get; private set; }
    public DateTime AssignedAt { get; private set; }
    public PlatformRoleStatus Status { get; private set; }
    private UserPlatformRole() { }
    public UserPlatformRole(int id, Guid userId, PlatformRole role)
        : base(id) 
    {
        UserId = userId;
        Role = role;
        AssignedAt = DateTime.UtcNow;
        Status = PlatformRoleStatus.Active;
    }
    public void ChangeRole(PlatformRole newRole)
    {
        if (Role == newRole) return;
        Role = newRole;
        MarkAsUpdated();
    }
    public void Revoke()
    {
        if (Status == PlatformRoleStatus.Revoked) return;
        Status = PlatformRoleStatus.Revoked;
    }
}
