using MediatR;
using SkillHub.Modules.Tenancy.Application.Abstractions.Authentication;
using SkillHub.Modules.Tenancy.Application.Abstractions.Persistence;
using SkillHub.Modules.Tenancy.Application.Exceptions;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Queries.GetTenant;

public sealed class GetTenantQueryHandler : IRequestHandler<GetTenantQuery, GetTenantResponse>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentUserTenancy _currentUserTenancy;

    public GetTenantQueryHandler(
        ITenantRepository tenantRepository,
        ICurrentUserTenancy currentUserTenancy)
    {
        _tenantRepository = tenantRepository;
        _currentUserTenancy = currentUserTenancy;
    }

    public async Task<GetTenantResponse> Handle(
        GetTenantQuery request,
        CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetTenantAsync(
            _currentUserTenancy.TenantId,
            cancellationToken);

        if (tenant is null)
            throw new TenantNotFoundException();

        return new GetTenantResponse
        {
            Name = tenant.Name,
            Slug = tenant.Slug,
            Status = tenant.Status
        };
    }
}
