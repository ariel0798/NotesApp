using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand,Result<JwtToken>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<Result<JwtToken>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (request.RefreshToken == null)
            return new Result<JwtToken>(ExceptionFactory.InvalidCredentialException);

        var user = await _unitOfWork.Users.GetUserByRefreshToken(request.RefreshToken);
        
        if (user == null)
            return new Result<JwtToken>(ExceptionFactory.InvalidCredentialException);

        if (user.TokenExpires < DateTime.Now)
            return new Result<JwtToken>(ExceptionFactory.InvalidCredentialException);
        
        var fullToken = await SetTokenAndRefreshToken(user);
        
        return fullToken;
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