

using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.Languages.Commands.RemoveLanguage;

public sealed record RemoveLanguageCommand(int id) : IRequest<RemoveLanguageResponse> ;
