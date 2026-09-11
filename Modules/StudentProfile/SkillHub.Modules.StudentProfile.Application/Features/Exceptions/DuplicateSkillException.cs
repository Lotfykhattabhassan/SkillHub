

namespace SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

public class DuplicateSkillException : Exception
{
    public DuplicateSkillException()
        : base("this skill already exists.")
    {
    }
}
