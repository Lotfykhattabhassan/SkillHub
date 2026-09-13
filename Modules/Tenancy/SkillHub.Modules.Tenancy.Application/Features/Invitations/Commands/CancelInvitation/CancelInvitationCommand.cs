
using MediatR;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.CancelInvitation;

public sealed record CancelInvitationCommand(int InvitationId) : IRequest;
