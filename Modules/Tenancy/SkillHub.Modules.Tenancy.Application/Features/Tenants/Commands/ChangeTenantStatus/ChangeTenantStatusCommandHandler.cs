using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.ChangeTenantStatus;

public sealed class ChangeTenantStatusCommandHandler : IRequestHandler<ChangeTenantStatusCommand, ChangeTenantStatusResponse>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly ITenantAuthorizationService _authorization;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeTenantStatusCommandHandler(
        ITenantRepository tenantRepository,
        ICurrentUserTenancy currentUserTenancy,
        ITenantAuthorizationService authorization,
        IUnitOfWork unitOfWork)
    {
        _tenantRepository = tenantRepository;
        _currentUserTenancy = currentUserTenancy;
        _authorization = authorization;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChangeTenantStatusResponse> Handle(
        ChangeTenantStatusCommand request,
        CancellationToken cancellationToken)
    {
        await _authorization.EnsureAnyRoleAsync(cancellationToken, MembershipRole.Owner, MembershipRole.Admin);

        var tenant = await _tenantRepository.GetTenantAsync(
            _currentUserTenancy.TenantId,
            cancellationToken);

        if (tenant is null)
            throw new TenantNotFoundException();

        switch (request.Status)
        {
            case TenantStatus.Active:
                tenant.Activate();
                break;
            case TenantStatus.Suspended:
                tenant.Suspend();
                break;
            case TenantStatus.Deactivated:
                tenant.Deactivate();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(request.Status));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ChangeTenantStatusResponse { TenantId = tenant.Id };
    }
}
