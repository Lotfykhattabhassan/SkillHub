namespace SkillHub.Modules.Tenancy.Application.Exceptions;

public sealed class InvitationAccessDeniedException : Exception
{
    public InvitationAccessDeniedException() : base("This invitation does not belong to the current user.") { }
}
