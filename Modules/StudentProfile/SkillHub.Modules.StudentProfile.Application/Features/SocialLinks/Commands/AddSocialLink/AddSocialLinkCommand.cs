

using MediatR;
using SkillHub.Modules.StudentProfile.Domain.Enums;

namespace SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.AddSocialLink;

public sealed record AddSocialLinkCommand(
        SocialLinkPlatform platform,
        string url
    ) : IRequest<AddSocialLinkResponse>;

