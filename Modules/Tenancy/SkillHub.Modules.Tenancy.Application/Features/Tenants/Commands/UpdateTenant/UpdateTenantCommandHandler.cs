using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.UpdateTenant;

public sealed class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, UpdateTenantResponse>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;
    private readonly ITenantAuthorizationService _authorization;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTenantCommandHandler(
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

    public async Task<UpdateTenantResponse> Handle(
        UpdateTenantCommand request,
        CancellationToken cancellationToken)
    {
        await _authorization.EnsureAnyRoleAsync(cancellationToken, MembershipRole.Owner, MembershipRole.Admin);

        var tenant = await _tenantRepository.GetTenantAsync(
            _currentUserTenancy.TenantId,
            cancellationToken);

        if (tenant is null)
            throw new TenantNotFoundException();

        tenant.Rename(request.Name);
        tenant.ChangeSlug(request.Slug);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateTenantResponse { TenantId = tenant.Id };
    }
}
