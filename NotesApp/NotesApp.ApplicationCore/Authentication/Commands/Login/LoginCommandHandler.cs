using System.Security.Authentication;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand,Result<JwtToken>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<JwtToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(request.Email);
        
        if (user == null)
            return new Result<JwtToken>().CreateException<JwtToken,InvalidCredentialException>();

        if (!_passwordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return new Result<JwtToken>().CreateException<JwtToken,InvalidCredentialException>();

        var fullToken = await SetTokenAndRefreshToken(user);

        return fullToken;
    }
    
    private async Task<User?> GetUserByEmail(string email)
    {
        var users = await _userRepository.GetAll();

        foreach (var user in users)
        {
            if (user.Email.Equals(email))
                return user;
        }
        return null;
    }
    private async Task<JwtToken> SetTokenAndRefreshToken(User user)
    {
        var token = _jwtTokenGenerator.CreateToken(user.Email);

        var fullToken = _jwtTokenGenerator.GenerateRefreshToken();
        fullToken.Token = token;

        user.RefreshToken = fullToken.RefreshToken;
        user.TokenCreated = fullToken.Created;
        user.TokenExpires = fullToken.Expires;
        
        await _userRepository.Update(user);

        return fullToken;
    }
}