namespace SkillHub.Modules.Tenancy.Application.Exceptions;

public sealed class MembershipAlreadyExistsException : Exception
{
    public MembershipAlreadyExistsException() : base("The user already has a membership in this tenant.") { }
}
