using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Helper.Interfaces;
using NotesApp.ApplicationCore.Models;

namespace NotesApp.ApplicationCore.Helper;

public class JwtHelper : IJwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string CreateToken(string email)
    {
        List<Claim> claims = new List<Claim> 
        { 
            new Claim(ClaimTypes.Name,email)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSetting:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;    
    }

    public JwtToken GenerateRefreshToken()
    {
        var refreshToken = new JwtToken()
        {
            RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
        };
        return refreshToken;
    }
}