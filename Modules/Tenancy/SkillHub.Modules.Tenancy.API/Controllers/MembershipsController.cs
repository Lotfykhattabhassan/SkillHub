using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.AddMember;
using SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.ChangeMemberRole;
using SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.ChangeMemberStatus;
using SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.RemoveMember;
using SkillHub.Modules.Tenancy.Application.Features.Memberships.Queries.GetMembers;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.API.Controllers;

[ApiController]
[Authorize]
[Route("api/memberships")]
public sealed class MembershipsController : ControllerBase
{
    private readonly ISender _sender;
    public MembershipsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetMembers(CancellationToken cancellationToken)
        => Ok(await _sender.Send(new GetMembersQuery(), cancellationToken));

    [HttpPost]
    public async Task<IActionResult> AddMember(
        [FromBody] AddMemberCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);
        return Created($"/api/memberships/{response.MembershipId}", response);
    }

    [HttpPatch("{id:int}/role")]
    public async Task<IActionResult> ChangeRole(
        int id,
        [FromBody] ChangeMemberRoleRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(
            new ChangeMemberRoleCommand(id, request.Role), cancellationToken);
        return Ok(response);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> ChangeStatus(
        int id,
        [FromBody] ChangeMemberStatusRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(
            new ChangeMemberStatusCommand(id, request.Status), cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(
        int id, CancellationToken cancellationToken)
    {
        await _sender.Send(new RemoveMemberCommand(id), cancellationToken);
        return NoContent();
    }
}

public sealed record ChangeMemberRoleRequest(MembershipRole Role);
public sealed record ChangeMemberStatusRequest(MembershipStatus Status);
