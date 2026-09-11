

using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.ChangeProfileVisibility;

public sealed record ChangeProfileVisibilityCommand(bool isPublic)
    : IRequest<ChangeProfileVisibilityResponse>;

