using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;
using SkillHub.Modules.Tenancy.Domain.Entities;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.AcceptInvitation;

public sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand>
{
    private readonly ITenantInvitationRepository _invitationRepository;
    private readonly ITenantMembershipRepository _membershipRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptInvitationCommandHandler(
        ITenantInvitationRepository invitationRepository,
        ITenantMembershipRepository membershipRepository,
        ICurrentUserTenancy currentUserTenancy,
        IUnitOfWork unitOfWork)
    {
        _invitationRepository = invitationRepository;
        _membershipRepository = membershipRepository;
        _currentUserTenancy = currentUserTenancy;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var invitation = await _invitationRepository.GetInvitationForAcceptanceAsync(
            request.InvitationId, cancellationToken);

        if (invitation is null)
            throw new InvitationNotFoundException();

        if (!string.Equals(invitation.Email, _currentUserTenancy.Email, StringComparison.OrdinalIgnoreCase))
            throw new InvitationAccessDeniedException();

        invitation.Expire();
        if (invitation.Status == Domain.Enums.InvitationStatus.Expired)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            throw new InvitationExpiredException();
        }

        if (invitation.Status != Domain.Enums.InvitationStatus.Pending)
            throw new InvitationNotFoundException();

        var existingMembership = await _membershipRepository.GetByUserAndTenantAsync(
            _currentUserTenancy.UserId, invitation.TenantId, cancellationToken);

        if (existingMembership is not null)
            throw new MembershipAlreadyExistsException();

        invitation.Accept();

        var membership = TenantMembership.Create(
            _currentUserTenancy.UserId, invitation.TenantId, invitation.Role);
        membership.Accept();

        await _membershipRepository.AddMemberAsync(membership, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
