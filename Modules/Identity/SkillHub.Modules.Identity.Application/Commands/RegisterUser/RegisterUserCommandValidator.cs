using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SkillHub.Modules.Identity.Application.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50)
            .Matches(@"^[\p{L}]+$")
            .WithMessage("First name must contain letters only, without numbers or symbols.");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("last name is required.")
            .MaximumLength(50)
            .Matches(@"^[\p{L}]+$")
            .WithMessage("First name must contain letters only, without numbers or symbols.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(32).WithMessage("Password cannot exceed 32 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character (e.g., @, #, $, %, etc.).");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+[1-9]\d{1,14}$").WithMessage("Invalid international phone number format. Use international format (e.g., +201001234567).");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.Today).WithMessage("Date of birth cannot be in the future.")
            .Must(dob => dob <= DateTime.Today.AddYears(-18)).WithMessage("You must be at least 18 years old.");

        RuleFor(x => x.UserType)
            .IsInEnum().WithMessage("Invalid user type selected.");

    }
}
