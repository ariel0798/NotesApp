using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;
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
    private readonly IHashids _hashids;
    
    public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, IHashids hashids)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _hashids = hashids;
    }
    public async Task<Result<JwtToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(request.Email.ToLower());
        
        if (user == null)
            return new Result<JwtToken>(ExceptionFactory.InvalidCredentialException);

        if (!_passwordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return new Result<JwtToken>(ExceptionFactory.InvalidCredentialException);

        var fullToken = await SetTokenAndRefreshToken(user,_jwtTokenGenerator,_unitOfWork, _hashids);

        return fullToken;
    }
    
    private async Task<User?> GetUserByEmail(string email)
    {
        return  await _unitOfWork.Users.GetUserByEmail(email);
    }
    
    public static async Task<JwtToken> SetTokenAndRefreshToken(User user, IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork, IHashids hashids)
    {
        var codeId = hashids.Encode(user.UserId);
        var token = jwtTokenGenerator.CreateToken(user.Email, codeId);

        var fullToken = jwtTokenGenerator.GenerateRefreshToken();
        fullToken.Token = token;

        user.RefreshToken = fullToken.RefreshToken;
        user.TokenCreated = fullToken.Created;
        user.TokenExpires = fullToken.Expires;
        
        unitOfWork.Users.Update(user);
        await unitOfWork.Save();

        return fullToken;
    }
}