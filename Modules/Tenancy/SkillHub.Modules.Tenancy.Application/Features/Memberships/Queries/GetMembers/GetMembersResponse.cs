using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Queries.GetMembers;

public sealed record GetMembersResponse(
    int Id,
    Guid UserId,
    MembershipRole Role,
    MembershipStatus Status,
    DateTime JoinedAt);
