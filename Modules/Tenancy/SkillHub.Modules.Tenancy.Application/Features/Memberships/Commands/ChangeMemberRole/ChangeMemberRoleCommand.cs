using MediatR;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.ChangeMemberRole;

public sealed record ChangeMemberRoleCommand(
    int Id,
    MembershipRole Role) : IRequest<ChangeMemberRoleResponse>;
