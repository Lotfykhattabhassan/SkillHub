using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Domain.Entities;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.CreateTenant;

public sealed class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, CreateTenantCommandResponse>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantMembershipRepository _membershipRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTenantCommandHandler(
        ITenantRepository tenantRepository,
        ITenantMembershipRepository membershipRepository,
        ICurrentUserTenancy currentUserTenancy,
        IUnitOfWork unitOfWork)
    {
        _tenantRepository = tenantRepository;
        _membershipRepository = membershipRepository;
        _currentUserTenancy = currentUserTenancy;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateTenantCommandResponse> Handle(
        CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = Tenant.Create(request.Name, request.Slug);
        await _tenantRepository.AddTenantAsync(tenant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var ownerMembership = TenantMembership.Create(
            _currentUserTenancy.UserId, tenant.Id, MembershipRole.Owner);
        ownerMembership.Accept();

        await _membershipRepository.AddMemberAsync(ownerMembership, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateTenantCommandResponse { TenantId = tenant.Id };
    }
}
