using System;
using MediatR;
using SkillHub.Modules.Identity.Application.Abstractions.Persistence;
using SkillHub.Modules.Identity.Application.Abstractions.Security;
using SkillHub.Modules.Identity.Domain.Entities;

namespace SkillHub.Modules.Identity.Application.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand , Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserCredentialRepository _userCredentialRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserCommandHandler(IUserRepository userRepository,
        IUserCredentialRepository userCredentialRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userCredentialRepository = userCredentialRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if ( exists != null ) throw new Exception("Email already exists");

        var hashedPassword = _passwordHasher.Hash(request.Password);
        var user = User.Create(request.FirstName, request.LastName,request.Email, request.Phone, request.DateOfBirth, request.UserType);
        await _userRepository.AddAsync(user, cancellationToken);

        var userCredential = UserCredential.Create(user.Id, hashedPassword);
        await _userCredentialRepository.AddAsync(userCredential, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

}
