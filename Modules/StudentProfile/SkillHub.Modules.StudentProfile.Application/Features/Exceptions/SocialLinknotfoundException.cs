

namespace SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

public class SocialLinknotfoundException : Exception
{
    public SocialLinknotfoundException()
        : base("Social Link was not found.")
    {
    }
}
