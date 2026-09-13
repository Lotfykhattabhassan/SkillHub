namespace SkillHub.Modules.Tenancy.Application.Exceptions;

public sealed class InvitationExpiredException : Exception
{
    public InvitationExpiredException() : base("Invitation has expired.") { }
}
