using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SkillHub.Modules.Identity.Application.Queries.GetUserById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.userId)
            .NotEmpty()
            .WithMessage("User ID is required.");
    }
    
}
