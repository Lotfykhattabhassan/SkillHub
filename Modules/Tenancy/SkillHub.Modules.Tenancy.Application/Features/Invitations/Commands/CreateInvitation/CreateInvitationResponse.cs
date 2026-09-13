namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.CreateInvitation;

public sealed class CreateInvitationResponse
{
    public int InvitationId { get; init; }
    public int TenantId { get; init; }
}
