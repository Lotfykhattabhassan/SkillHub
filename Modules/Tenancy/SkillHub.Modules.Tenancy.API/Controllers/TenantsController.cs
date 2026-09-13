using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.ChangeTenantStatus;
using SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.CreateTenant;
using SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.UpdateTenant;
using SkillHub.Modules.Tenancy.Application.Features.Tenants.Queries.GetTenant;

namespace SkillHub.Modules.Tenancy.API.Controllers;

[ApiController]
[Authorize]
[Route("api/tenants")]
public sealed class TenantsController : ControllerBase
{
    private readonly ISender _sender;
    public TenantsController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTenantCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);
        return Created($"/api/tenants/current", response);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
        => Ok(await _sender.Send(new GetTenantQuery(), cancellationToken));

    [HttpPatch("current")]
    public async Task<IActionResult> Update(
        [FromBody] UpdateTenantCommand request,
        CancellationToken cancellationToken)
        => Ok(await _sender.Send(request, cancellationToken));

    [HttpPatch("current/status")]
    public async Task<IActionResult> ChangeStatus(
        [FromBody] ChangeTenantStatusCommand request,
        CancellationToken cancellationToken)
        => Ok(await _sender.Send(request, cancellationToken));
}
