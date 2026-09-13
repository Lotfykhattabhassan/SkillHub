using FluentValidation;
using SkillHub.Modules.Tenancy.Domain.Enums;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.ChangeMemberStatus;

public sealed class ChangeMemberStatusValidator : AbstractValidator<ChangeMemberStatusCommand>
{
    public ChangeMemberStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Status).IsInEnum().Must(x => x != MembershipStatus.Pending).WithMessage("Pending is not a valid target status.");
    }
}
