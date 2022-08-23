using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Commands.Login;
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
    private readonly IHashids _hashids;
    
    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator, IHashids hashids)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
        _hashids = hashids;
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
        
        var fullToken = await LoginCommandHandler.SetTokenAndRefreshToken(user, _jwtTokenGenerator,_unitOfWork, _hashids);
        
        return fullToken;
    }
    
    
}