using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;
using SkillHub.Modules.Identity.Application.Commands.LoginUser;
using SkillHub.Modules.Identity.Application.Exceptions;

namespace SkillHub.Modules.Identity.Application.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, GetCurrentUserResponse>
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserRepository _userRepository;
    public GetCurrentUserQueryHandler(ICurrentUser currentUser,
        IUserRepository userRepository)
    {
        _currentUser = currentUser;
        _userRepository = userRepository;
    }
    public async Task<GetCurrentUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;
        if (userId is null)
            throw new UnauthorizedAccessException("User is not authenticated.");
        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null) throw new UserNotFoundException();
        return new GetCurrentUserResponse
        {
            Id = user.Id,
            Email = user.Email,
            UserType = user.UserType
        };
    }
}
