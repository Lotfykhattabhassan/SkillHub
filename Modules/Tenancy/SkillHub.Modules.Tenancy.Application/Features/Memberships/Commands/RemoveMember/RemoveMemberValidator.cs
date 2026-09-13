using FluentValidation;

namespace SkillHub.Modules.Tenancy.Application.Features.Memberships.Commands.RemoveMember;

public sealed class RemoveMemberValidator : AbstractValidator<RemoveMemberCommand>
{
    public RemoveMemberValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
