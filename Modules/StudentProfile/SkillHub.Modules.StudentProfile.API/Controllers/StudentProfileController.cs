using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.ChangeProfileImage;
using SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.ChangeProfileVisibility;
using SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.CreateStudentProfile;
using SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.UpdateAcademicInformation;
using SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.UpdateBasicInformation;
using SkillHub.Modules.StudentProfile.Application.Features.Profiles.Queries.GetMyProfile;
using SkillHub.Modules.StudentProfile.Application.Features.Profiles.Queries.GetStudentProfile;

namespace SkillHub.Modules.StudentProfile.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentProfileController : ControllerBase
    {
        private readonly ISender _sender;
        public StudentProfileController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudentProfile(
            [FromBody] CreateStudentProfileCommand request,
            CancellationToken cancellationToken)
        {
            var studentProfile = await _sender.Send(request, cancellationToken);
            return Created($"/api/StudentProfile/{studentProfile.Id}", studentProfile);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile(CancellationToken cancellationToken)
        {
            var studentProfile = await _sender.Send(
                new GetMyProfileQuery()
                , cancellationToken);

            return Ok(studentProfile);
        }
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetStudentProfile(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var studentProfile = await _sender.Send(
                new GetStudentProfileQuery(userId)
                , cancellationToken);

            return Ok(studentProfile);
        }
        [HttpPut("me/basic-information")]
        public async Task<IActionResult> UpdateBasicInformation (
            [FromBody] UpdateBasicInformationCommand request,
            CancellationToken cancellationToken
            )
        {
            await _sender.Send(request, cancellationToken);
            return NoContent();
        }
        [HttpPut("me/academic-information")]
        public async Task<IActionResult> UpdateAcademicInformation(
            [FromBody] UpdateAcademicInformationCommand request,
            CancellationToken cancellationToken
            )
        {
            await _sender.Send(request, cancellationToken);
            return NoContent();
        }
        [HttpPut("me/image")]
        public async Task<IActionResult> ChangeProfileImage(
            [FromBody] ChangeProfileImageCommand request,
            CancellationToken cancellationToken
            )
        {
            await _sender.Send(request, cancellationToken);
            return NoContent();
        }
        [HttpPatch("me/visibility")]
        public async Task<IActionResult> ChangeProfileVisibility(
        [FromBody] ChangeProfileVisibilityCommand request,
        CancellationToken cancellationToken
        )
        {
            await _sender.Send(request, cancellationToken);
            return NoContent();
        }
        }
}
