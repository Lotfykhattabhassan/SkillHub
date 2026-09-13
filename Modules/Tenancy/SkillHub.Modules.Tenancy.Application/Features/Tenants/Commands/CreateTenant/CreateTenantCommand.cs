using MediatR;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.CreateTenant;

public sealed record CreateTenantCommand(
    string Name,
    string Slug) : IRequest<CreateTenantCommandResponse>;
