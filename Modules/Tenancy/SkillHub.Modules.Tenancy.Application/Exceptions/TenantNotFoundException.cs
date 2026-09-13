
namespace SkillHub.Modules.Tenancy.Application.Exceptions;

public class TenantNotFoundException : Exception
{
    public TenantNotFoundException() : base("Tenant not found.")
    {

    }
}
