using MediatR;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.AcceptInvitation;

public sealed record AcceptInvitationCommand(int InvitationId) : IRequest;
