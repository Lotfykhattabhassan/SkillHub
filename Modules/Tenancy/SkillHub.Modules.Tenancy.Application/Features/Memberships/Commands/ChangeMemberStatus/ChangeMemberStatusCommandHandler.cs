using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.ChangeMemberStatus;

public sealed class ChangeMemberStatusCommandHandler : IRequestHandler<ChangeMemberStatusCommand, ChangeMemberStatusResponse>
{
    private readonly ITenantMembershipRepository _membershipRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly ITenantAuthorizationService _authorization;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeMemberStatusCommandHandler(
        ITenantMembershipRepository membershipRepository,
        ICurrentUserTenancy currentUserTenancy,
        ITenantAuthorizationService authorization,
        IUnitOfWork unitOfWork)
    {
        _membershipRepository = membershipRepository;
        _currentUserTenancy = currentUserTenancy;
        _authorization = authorization;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChangeMemberStatusResponse> Handle(
        ChangeMemberStatusCommand request,
        CancellationToken cancellationToken)
    {
        await _authorization.EnsureAnyRoleAsync(cancellationToken, MembershipRole.Owner, MembershipRole.Admin);

        var membership = await _membershipRepository.GetByIdAsync(
            request.Id,
            _currentUserTenancy.TenantId,
            cancellationToken);

        if (membership is null)
            throw new MembershipNotFoundException();

        var currentMembership = await _authorization.GetCurrentMembershipAsync(cancellationToken);
        if ((membership.Role & MembershipRole.Owner) == MembershipRole.Owner &&
            (currentMembership.Role & MembershipRole.Owner) != MembershipRole.Owner)
            throw new ForbiddenTenantOperationException();

        switch (request.Status)
        {
            case MembershipStatus.Active:
                membership.Accept();
                break;
            case MembershipStatus.Suspended:
                membership.Suspend();
                break;
            case MembershipStatus.Revoked:
                membership.Revoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(request.Status));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new ChangeMemberStatusResponse { Id = membership.Id };
    }
}
