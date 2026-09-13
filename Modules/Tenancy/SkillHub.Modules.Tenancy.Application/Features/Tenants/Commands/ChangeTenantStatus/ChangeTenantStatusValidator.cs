using FluentValidation;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.ChangeTenantStatus;

public sealed class ChangeTenantStatusValidator : AbstractValidator<ChangeTenantStatusCommand>
{
    public ChangeTenantStatusValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
