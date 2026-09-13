namespace SkillHub.Modules.Tenancy.Application.Exceptions;

public sealed class ForbiddenTenantOperationException : Exception
{
    public ForbiddenTenantOperationException() : base("You do not have permission to perform this operation in this tenant.") { }
}
