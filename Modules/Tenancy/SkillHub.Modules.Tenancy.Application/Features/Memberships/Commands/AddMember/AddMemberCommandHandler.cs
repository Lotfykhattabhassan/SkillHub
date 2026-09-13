using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;
using SkillHub.Modules.Tenancy.Domain.Entities;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.AddMember;

public sealed class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, AddMemberResponse>
{
    private readonly ITenantMembershipRepository _tenantMembershipRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly ITenantAuthorizationService _authorization;
    private readonly IUnitOfWork _unitOfWork;

    public AddMemberCommandHandler(
        ITenantMembershipRepository tenantMembershipRepository,
        ICurrentUserTenancy currentUserTenancy,
        ITenantAuthorizationService authorization,
        IUnitOfWork unitOfWork)
    {
        _tenantMembershipRepository = tenantMembershipRepository;
        _currentUserTenancy = currentUserTenancy;
        _authorization = authorization;
        _unitOfWork = unitOfWork;
    }

    public async Task<AddMemberResponse> Handle(
        AddMemberCommand request,
        CancellationToken cancellationToken)
    {
        await _authorization.EnsureAnyRoleAsync(cancellationToken, MembershipRole.Owner, MembershipRole.Admin);

        var existingMembership = await _tenantMembershipRepository.GetByUserAndTenantAsync(
            request.UserId,
            _currentUserTenancy.TenantId,
            cancellationToken);

        if (existingMembership is not null)
            throw new MembershipAlreadyExistsException();

        var membership = TenantMembership.Create(
            request.UserId,
            _currentUserTenancy.TenantId);
        membership.Accept();

        await _tenantMembershipRepository.AddMemberAsync(
            membership,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AddMemberResponse { MembershipId = membership.Id };
    }
}
