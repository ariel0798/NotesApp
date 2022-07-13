using System.Security.Authentication;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<JwtToken>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<JwtToken>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (request.RefreshToken == null)
            return new Result<JwtToken>().CreateException<JwtToken,InvalidCredentialException>();

        var user = await GetUserByRefreshToken(request.RefreshToken);
        
        if (user == null)
            return new Result<JwtToken>().CreateException<JwtToken,InvalidCredentialException>();

        if (user.TokenExpires < DateTime.Now)
            return new Result<JwtToken>().CreateException<JwtToken,InvalidCredentialException>();
        
        var fullToken = await SetTokenAndRefreshToken(user);
        
        return fullToken;
    }

    private async Task<User?> GetUserByRefreshToken(string refreshToken)
    {
        var users = await _userRepository.GetAll();

        foreach (var user in users)
        {
            if (user.RefreshToken != null)
            {
                if (user.RefreshToken.Equals(refreshToken))
                    return user;
            }
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