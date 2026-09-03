using System;
using MediatR;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;
using SkillHub.Modules.Identity.Application.Abstractions.Security;
using SkillHub.Modules.Identity.Application.Exceptions;

namespace SkillHub.Modules.Identity.Application.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginUserResponse> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException(
                "Email and password must be provided.");
        }

        var user = await _userRepository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        if (user is null)
            throw new InvalidCredentialsException();

        var credential = user.UserCredential;

        if (credential is null)
            throw new InvalidOperationException(
                "User credentials were not found.");

        credential.CanLogin();

        var isPasswordValid = _passwordHasher.Verify(
            request.Password,
            credential.PasswordHash);

        if (!isPasswordValid)
        {
            credential.FailedLogin();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            throw new InvalidCredentialsException();
        }

        credential.SuccessfulLogin();
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserType);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new LoginUserResponse
        {
            AccessToken = token.AccessToken,
            ExpiresAt = token.ExpiresAt,
            TokenType = "Bearer"
        };
    }
}
