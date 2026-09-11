

namespace SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

public class SkillnotfoundException : Exception
{
    public SkillnotfoundException()
        : base("Skill was not found.")
    {
    }
}
