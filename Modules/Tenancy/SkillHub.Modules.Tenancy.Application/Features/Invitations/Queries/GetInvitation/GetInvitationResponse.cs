using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Queries.GetInvitation;

public sealed record GetInvitationResponse(int Id,
    int TenantId,
    string Email,
    MembershipRole Role,
    DateTime InvitedAt,
    DateTime ExpiresAt,
    InvitationStatus Status)
{
}
