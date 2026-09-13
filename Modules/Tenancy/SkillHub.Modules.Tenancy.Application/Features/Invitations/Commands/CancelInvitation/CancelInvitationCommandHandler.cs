using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.CancelInvitation;

public sealed class CancelInvitationCommandHandler : IRequestHandler<CancelInvitationCommand>
{
    private readonly ITenantInvitationRepository _invitationRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly ITenantAuthorizationService _authorization;
    private readonly IUnitOfWork _unitOfWork;

    public CancelInvitationCommandHandler(
        ITenantInvitationRepository invitationRepository,
        ICurrentUserTenancy currentUserTenancy,
        ITenantAuthorizationService authorization,
        IUnitOfWork unitOfWork)
    {
        _invitationRepository = invitationRepository;
        _currentUserTenancy = currentUserTenancy;
        _authorization = authorization;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        CancelInvitationCommand request,
        CancellationToken cancellationToken)
    {
        await _authorization.EnsureAnyRoleAsync(cancellationToken, MembershipRole.Owner, MembershipRole.Admin);

        var invitation = await _invitationRepository.GetInvitationAsync(
            request.InvitationId,
            _currentUserTenancy.TenantId,
            cancellationToken);

        if (invitation is null)
            throw new InvitationNotFoundException();

        invitation.Revoke();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
