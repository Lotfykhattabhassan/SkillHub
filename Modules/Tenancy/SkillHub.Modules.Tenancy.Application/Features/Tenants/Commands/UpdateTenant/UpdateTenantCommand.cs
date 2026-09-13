using MediatR;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.UpdateTenant;

public sealed record UpdateTenantCommand(
    string Name,
    string Slug) : IRequest<UpdateTenantResponse>;
