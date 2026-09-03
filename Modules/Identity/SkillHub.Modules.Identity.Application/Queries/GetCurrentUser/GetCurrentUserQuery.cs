using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SkillHub.Modules.Identity.Application.Queries.GetCurrentUser;

public sealed record GetCurrentUserQuery() : IRequest<GetCurrentUserResponse>;

