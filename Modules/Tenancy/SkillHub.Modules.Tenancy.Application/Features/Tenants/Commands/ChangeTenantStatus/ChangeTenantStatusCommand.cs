using MediatR;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.ChangeTenantStatus;

public sealed record ChangeTenantStatusCommand(
    TenantStatus Status) : IRequest<ChangeTenantStatusResponse>;
