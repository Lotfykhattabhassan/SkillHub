
using FluentValidation;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.CancelInvitation;

public class CancelInvitationValidator : AbstractValidator<CancelInvitationCommand>
{
    public CancelInvitationValidator()
    {
        RuleFor(x => x.InvitationId)
            .GreaterThan(0)
           .WithMessage("Invitation id must be greater than 0")
           ;
    }
}
