
using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.ChangeSocialLinkUrl;

public sealed record ChangeSocialLinkUrlCommand(
    int socialLinkId,
    string url) : IRequest<ChangeSocialLinkUrlResponse>
{
}
