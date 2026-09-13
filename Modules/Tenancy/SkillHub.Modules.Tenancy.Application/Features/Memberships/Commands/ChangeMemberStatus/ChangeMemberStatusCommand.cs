using MediatR;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.ChangeMemberStatus;

public sealed record ChangeMemberStatusCommand(
    int Id,
    MembershipStatus Status) : IRequest<ChangeMemberStatusResponse>;
