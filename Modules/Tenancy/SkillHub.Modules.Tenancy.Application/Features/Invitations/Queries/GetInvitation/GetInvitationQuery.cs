
using MediatR;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Queries.GetInvitation;

public sealed record GetInvitationQuery(int InvitationId) : IRequest<GetInvitationResponse>;

