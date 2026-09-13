
using FluentValidation;

namespace SkillHub.Modules.Tenancy.Application.Features.Invitations.Commands.AcceptInvitation;

public class AcceptInvitationValidator : AbstractValidator<AcceptInvitationCommand>
{
    public AcceptInvitationValidator()
    {
        RuleFor(x => x.InvitationId).GreaterThan(0)
            .WithMessage("Invitation id must be greater than 0")
            ;
    }
}
