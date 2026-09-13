
namespace SkillHub.Modules.Tenancy.Application.Exceptions;

public class TenantAccessDeniedException : Exception
{
    public TenantAccessDeniedException() : base("You do not have an active membership in this tenant.")
    {

    }
}
