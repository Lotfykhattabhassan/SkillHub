

using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.UpdateBasicInformation;

public sealed record UpdateBasicInformationCommand(
    string fullName,
    string bio
    ) : IRequest<UpdateBasicInformationResponse> ;
