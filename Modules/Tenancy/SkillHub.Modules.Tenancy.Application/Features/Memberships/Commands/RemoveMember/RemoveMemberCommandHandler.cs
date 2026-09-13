using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.RemoveMember;

public sealed class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand>
{
    private readonly ITenantMembershipRepository _membershipRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly ITenantAuthorizationService _authorization;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveMemberCommandHandler(
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

    public async Task Handle(
        RemoveMemberCommand request,
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

        _membershipRepository.RemoveMember(membership);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
