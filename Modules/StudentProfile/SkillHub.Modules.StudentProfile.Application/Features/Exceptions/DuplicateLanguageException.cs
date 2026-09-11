
namespace SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

public class DuplicateLanguageException : Exception
{
    public DuplicateLanguageException()
        : base("this language already exists.")
    {
    }
}
