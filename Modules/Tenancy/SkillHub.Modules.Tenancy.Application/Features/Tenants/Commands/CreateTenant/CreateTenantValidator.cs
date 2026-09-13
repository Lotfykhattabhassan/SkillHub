using FluentValidation;

namespace SkillHub.Modules.Tenancy.Application.Features.Tenants.Commands.CreateTenant;

public class CreateTenantValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("name is required")
            .MaximumLength(255);

        RuleFor(x=>x.Slug)
            .NotEmpty()
            .WithMessage("slug is required")
            .MaximumLength(255);
    }
}
