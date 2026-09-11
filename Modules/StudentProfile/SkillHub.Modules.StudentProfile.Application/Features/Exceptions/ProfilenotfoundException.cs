

namespace SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

public class ProfilenotfoundException : Exception
{
    public ProfilenotfoundException()
        : base("Profile was not found.")
    {
    }
}
