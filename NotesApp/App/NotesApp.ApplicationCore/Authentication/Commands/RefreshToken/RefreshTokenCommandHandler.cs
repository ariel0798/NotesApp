using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Commands.Login;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand,Result<JwtToken>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
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
        
        var fullToken = await _authService.SetTokenAndRefreshToken(user);
        
        return fullToken;
    }
    
    
}