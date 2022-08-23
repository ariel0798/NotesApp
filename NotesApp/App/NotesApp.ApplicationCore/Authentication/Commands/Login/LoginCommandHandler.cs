using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand,Result<JwtToken>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<Result<JwtToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(request.Email);
        
        if (user == null)
            return new Result<JwtToken>(ExceptionFactory.InvalidCredentialException);

        if (!_passwordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return new Result<JwtToken>(ExceptionFactory.InvalidCredentialException);

        var fullToken = await SetTokenAndRefreshToken(user);

        return fullToken;
    }
    
    private async Task<User?> GetUserByEmail(string email)
    {
        return  await _unitOfWork.Users.GetUserByEmail(email);
    }
    
    private async Task<JwtToken> SetTokenAndRefreshToken(User user)
    {
        var token = _jwtTokenGenerator.CreateToken(user.Email);

        var fullToken = _jwtTokenGenerator.GenerateRefreshToken();
        fullToken.Token = token;

        user.RefreshToken = fullToken.RefreshToken;
        user.TokenCreated = fullToken.Created;
        user.TokenExpires = fullToken.Expires;
        
        _unitOfWork.Users.Update(user);
        await _unitOfWork.Save();

        return fullToken;
    }
}