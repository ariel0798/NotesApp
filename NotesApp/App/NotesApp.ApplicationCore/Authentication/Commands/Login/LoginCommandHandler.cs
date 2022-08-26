using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand,Result<JwtToken>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthService _authService;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _authService = authService;
    }
    public async Task<Result<JwtToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(request.Email.ToLower());
        
        if (user == null)
            return new Result<JwtToken>(ExceptionFactory.InvalidCredentialException);

        if (!_passwordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return new Result<JwtToken>(ExceptionFactory.InvalidCredentialException);

        var fullToken = await _authService.SetTokenAndRefreshToken(user);

        return fullToken;
    }
    
    private async Task<User?> GetUserByEmail(string email)
    {
        return  await _unitOfWork.Users.GetUserByEmail(email);
    }
    

}