
namespace SkillHub.Modules.Tenancy.Application.Exceptions;

public class TenantContextMissingException : Exception
{
    public TenantContextMissingException() : base("Tenant context is missing or invalid.")
    {

    }
}
