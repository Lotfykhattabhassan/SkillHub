using MediatR;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.CreateInvitation;

public sealed record CreateInvitationCommand(
    string Email,
    MembershipRole Role,
    DateTime ExpiresAt) : IRequest<CreateInvitationResponse>;
