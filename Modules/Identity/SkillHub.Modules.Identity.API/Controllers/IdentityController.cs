using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Modules.Identity.Application.Commands.ChangePassword;
using SkillHub.Modules.Identity.Application.Commands.LoginUser;
using SkillHub.Modules.Identity.Application.Commands.RegisterUser;
using SkillHub.Modules.Identity.Application.Queries.GetCurrentUser;
using SkillHub.Modules.Identity.Application.Queries.GetUserById;
using SkillHub.Modules.Identity.Domain.Entities;

namespace SkillHub.Modules.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ISender _sender;
        public IdentityController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand request , CancellationToken cancellationToken)
        {

            var user = await _sender.Send(request, cancellationToken);
            return Created($"/api/Identity/{user}",
        user);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _sender.Send(request, cancellationToken);
           
            return Ok(user);
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var user = await _sender.Send(
                new GetCurrentUserQuery(),
                cancellationToken);

            return Ok(user);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var user = await _sender.Send(
                new GetUserByIdQuery(id),
                cancellationToken);

            return Ok(user);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _sender.Send(request,cancellationToken);
            return Ok(response);
        }
    }
}
