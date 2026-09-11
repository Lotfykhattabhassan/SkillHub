
using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.RemoveSocialLink;

public sealed record RemoveSocialLinkCommand(int id) : IRequest<RemoveSocialLinkResponse>;
