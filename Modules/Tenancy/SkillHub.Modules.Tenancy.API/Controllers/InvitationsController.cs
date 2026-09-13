using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.AcceptInvitation;
using SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.CancelInvitation;
using SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.CreateInvitation;
using SkillHub.Modules.Tenancy.Application.Features.Invitations.Queries.GetInvitation;

namespace SkillHub.Modules.Tenancy.API.Controllers;

[ApiController]
[Authorize]
[Route("api/invitations")]
public sealed class InvitationsController : ControllerBase
{
    private readonly ISender _sender;
    public InvitationsController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateInvitationCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);
        return Created($"/api/invitations/{response.InvitationId}", response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(
        int id, CancellationToken cancellationToken)
        => Ok(await _sender.Send(new GetInvitationQuery(id), cancellationToken));

    [HttpPost("{id:int}/accept")]
    public async Task<IActionResult> Accept(
        int id, CancellationToken cancellationToken)
    {
        await _sender.Send(new AcceptInvitationCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Cancel(
        int id, CancellationToken cancellationToken)
    {
        await _sender.Send(new CancelInvitationCommand(id), cancellationToken);
        return NoContent();
    }
}
