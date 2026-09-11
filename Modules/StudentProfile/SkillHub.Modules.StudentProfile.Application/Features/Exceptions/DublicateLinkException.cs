
namespace SkillHub.Modules.StudentProfile.Application.Features.Exceptions;

public class DublicateLinkException : Exception
{
    public DublicateLinkException()
        : base("this link already exists.")
    {
        
    }
}
