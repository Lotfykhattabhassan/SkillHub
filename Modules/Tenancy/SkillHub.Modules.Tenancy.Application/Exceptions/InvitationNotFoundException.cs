
namespace SkillHub.Modules.Tenancy.Application.Exceptions;

public class InvitationNotFoundException : Exception
{
    public InvitationNotFoundException() : base("Invitation not found.")
    {
        
    }
}
