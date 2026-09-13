using FluentValidation;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.ChangeMemberRole;

public sealed class ChangeMemberRoleValidator : AbstractValidator<ChangeMemberRoleCommand>
{
    public ChangeMemberRoleValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Role).IsInEnum();
    }
}
