using MediatR;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.RemoveMember;

public sealed record RemoveMemberCommand(int Id) : IRequest;
