
using FluentValidation;

namespace SkillHub.Modules.StudentProfile.Application.Features.SocialLinks.Commands.AddSocialLink;

public class AddSocialLinkValidator : AbstractValidator<AddSocialLinkCommand>
{
    public AddSocialLinkValidator()
    {
        
        RuleFor(x => x.platform)
            .IsInEnum()
            .WithMessage("Invalid social link platform."); 
        RuleFor(x => x.url)
            .NotEmpty()
            .WithMessage("URL is required.")
            .MaximumLength(500)
            .WithMessage("URL cannot exceed 500 characters.")
            .Must(BeAValidUrl).WithMessage("URL must be a valid URL.");
    }
    private static bool BeAValidUrl(string url) 
    { 
        return Uri
            .TryCreate(url,
            UriKind.Absolute, 
            out var uri) 
            && (uri.Scheme == Uri.UriSchemeHttp 
            || uri.Scheme == Uri.UriSchemeHttps); }
}
