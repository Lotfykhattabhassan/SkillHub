using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;
using SkillHub.Modules.Identity.Application.Exceptions;

namespace SkillHub.Modules.Identity.Application.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery , GetUserByIdResponse>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request , CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId,cancellationToken);
        if (user == null) throw new UserNotFoundException();

        return new GetUserByIdResponse
        {
            FullName = user.FullName,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Phone = user.Phone,
            UserType = user.UserType
        };
    }
}
