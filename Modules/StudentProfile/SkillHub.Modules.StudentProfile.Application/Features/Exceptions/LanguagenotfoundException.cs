

namespace SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

public class LanguagenotfoundException : Exception
{
    public LanguagenotfoundException()
        : base("Language was not found.")
    {
    }
}
