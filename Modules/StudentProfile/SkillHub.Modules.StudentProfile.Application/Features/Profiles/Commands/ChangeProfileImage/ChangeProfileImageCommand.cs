using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SkillHub.Modules.StudentProfile.Application.Features.Profiles.Commands.ChangeProfileImage;

public sealed record ChangeProfileImageCommand(
    string profilePhotoUrl)
    : IRequest<ChangeProfileImageResponse>;

