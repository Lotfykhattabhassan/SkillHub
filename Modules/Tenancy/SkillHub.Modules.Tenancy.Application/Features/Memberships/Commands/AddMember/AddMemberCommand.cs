using MediatR;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.AddMember;

public sealed record AddMemberCommand(
    Guid UserId) : IRequest<AddMemberResponse>;
