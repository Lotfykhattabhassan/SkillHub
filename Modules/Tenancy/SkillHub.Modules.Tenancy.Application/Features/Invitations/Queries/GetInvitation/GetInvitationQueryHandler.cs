using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Queries.GetInvitation;

public sealed class GetInvitationQueryHandler : IRequestHandler<GetInvitationQuery, GetInvitationResponse>
{
    private readonly ITenantInvitationRepository _tenantInvitationRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;

    public GetInvitationQueryHandler(
        ITenantInvitationRepository tenantInvitationRepository,
        ICurrentUserTenancy currentUserTenancy)
    {
        _tenantInvitationRepository = tenantInvitationRepository;
        _currentUserTenancy = currentUserTenancy;
    }

    public async Task<GetInvitationResponse> Handle(
        GetInvitationQuery request,
        CancellationToken cancellationToken)
    {
        var invitation = await _tenantInvitationRepository.GetInvitationAsync(
            request.InvitationId,
            _currentUserTenancy.TenantId,
            cancellationToken);

        if (invitation is null)
            throw new InvitationNotFoundException();

        return new GetInvitationResponse(
            invitation.Id,
            invitation.TenantId,
            invitation.Email,
            invitation.Role,
            invitation.InvitedAt,
            invitation.ExpiresAt,
            invitation.Status);
    }
}
