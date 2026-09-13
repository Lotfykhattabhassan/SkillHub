
namespace SkillHub.Modules.Tenancy.Application.Exceptions;

public class MembershipNotFoundException : Exception
{
    public MembershipNotFoundException() : base("Membership not found.")
    {
        
    }
}
