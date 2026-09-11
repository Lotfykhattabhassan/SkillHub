using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.AddSocialLink;
using SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.ChangeSocialLinkUrl;
using SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.RemoveSocialLink;

namespace SkillHub.Modules.StudentProfile.API.Controllers
{
    [ApiController]
    [Route("api/student-social-links")]
    public class StudentSocialLinksController : ControllerBase
    {
        private readonly ISender _sender;

        public StudentSocialLinksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("me")]
        public async Task<IActionResult> AddSocialLink(
            [FromBody] AddSocialLinkCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _sender.Send(
                request,
                cancellationToken);

            return Created(
                $"/api/student-social-links/me/{response.Id}",
                response);
        }

        [HttpDelete("me/{socialLinkId:int}")]
        public async Task<IActionResult> RemoveSocialLink(
            int socialLinkId,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new RemoveSocialLinkCommand(socialLinkId),
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("me/{socialLinkId:int}/url")]
        public async Task<IActionResult> ChangeSocialLinkUrl(
            int socialLinkId,
            [FromBody] ChangeSocialLinkUrlRequest request,
            CancellationToken cancellationToken)
        {
            await _sender.Send(
                new ChangeSocialLinkUrlCommand(
                    socialLinkId,
                    request.Url),
                cancellationToken);

            return NoContent();
        }
    }

    public sealed record ChangeSocialLinkUrlRequest(
        string Url);
}
