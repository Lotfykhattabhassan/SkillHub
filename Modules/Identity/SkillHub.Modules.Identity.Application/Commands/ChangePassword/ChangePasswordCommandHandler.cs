using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;
using SkillHub.Modules.Identity.Application.Abstractions.Security;
using SkillHub.Modules.Identity.Application.Exceptions;

namespace SkillHub.Modules.Identity.Application.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ICurrentUser _currentUser;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ChangePasswordCommandHandler(IPasswordHasher passwordHasher,
        ICurrentUser currentUser,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _passwordHasher = passwordHasher;
        _currentUser = currentUser;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChangePasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;
        if ( userId == null  ) throw new UnauthorizedAccessException("User is not authenticated.");

        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);
        if (user == null) throw new UserNotFoundException();

        user.UserCredential.CanLogin();

        bool isValid = _passwordHasher.Verify(request.Password, user.UserCredential.PasswordHash);
        if (!isValid) throw new UnauthorizedAccessException("Current password is incorrect.");

        var newHashedPassword = _passwordHasher.Hash(request.NewPassword);

        user.UserCredential.ChangePassword(newHashedPassword);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ChangePasswordResponse
        {
            Message = "Password changed successfully"
        };
    }
}

