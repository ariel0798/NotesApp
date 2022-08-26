using System.Security.Claims;
using HashidsNet;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Common.Models;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHashids _hashids;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HashIdSettings _hashIdSettings;

    public AuthService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator, IHashids hashids, 
        IHttpContextAccessor httpContextAccessor, IOptions<HashIdSettings> hashSettings)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
        _hashids = hashids;
        _httpContextAccessor = httpContextAccessor;
        _hashIdSettings = hashSettings.Value;
    }

    public async Task<JwtToken> SetTokenAndRefreshToken(User user)
    {
        var codeId = _hashids.Encode(user.UserId);
        var token = _jwtTokenGenerator.CreateToken(user.Email, codeId);

        var fullToken = _jwtTokenGenerator.GenerateRefreshToken();
        fullToken.Token = token;

        user.RefreshToken = fullToken.RefreshToken;
        user.TokenCreated = fullToken.Created;
        user.TokenExpires = fullToken.Expires;
        
        _unitOfWork.Users.Update(user);
        await _unitOfWork.Save();

        return fullToken;
    }

    public void SetRefreshToken(JwtToken refreshToken, HttpContext context)
    {
        var cookieOption = new CookieOptions()
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };
        
        context.Response.Cookies.Append("refreshToken", refreshToken.RefreshToken, cookieOption);
    }

    public int? GetUserIdByHttpContext()
    {
        var hashUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!IsIdRightLenght(hashUserId))
            return null;
        
        var decodedId = _hashids.Decode(hashUserId);
        return decodedId.Any() ? decodedId[0] : null;
    }
    
    public bool IsIdRightLenght(string id)
    {
        return id.Length >= _hashIdSettings.MinimumHashIdLenght;
    }

    public Result<T>? ValidateUserId<T>(int? userId)
    {
        if (userId == null)
            return new Result<T>(ExceptionFactory.InvalidCredentialException);

        return null;
    }
}