using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SkillHub.Modules.Identity.Application.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid userId) : IRequest<GetUserByIdResponse>;

