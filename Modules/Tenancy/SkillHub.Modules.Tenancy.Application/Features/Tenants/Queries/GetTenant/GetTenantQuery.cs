using MediatR;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Queries.GetTenant;

public sealed record GetTenantQuery : IRequest<GetTenantResponse>;
