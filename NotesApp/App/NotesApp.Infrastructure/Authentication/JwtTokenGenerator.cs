using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.Infrastructure.Services.Interfaces;

namespace NotesApp.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions, IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }
    public string CreateToken(string email)
    {
        List<Claim> claims = new List<Claim> 
        { 
            new Claim(ClaimTypes.Name,email)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: _dateTimeProvider.AddMinutes(_jwtSettings.ExpiryMinutes),
            issuer: _jwtSettings.Issuer,
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;    
    }

    public JwtToken GenerateRefreshToken()
    {
        var refreshToken = new JwtToken()
        {
            RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = _dateTimeProvider.AddDays(7),
            Created = _dateTimeProvider.Now
        };
        return refreshToken;
    }
}