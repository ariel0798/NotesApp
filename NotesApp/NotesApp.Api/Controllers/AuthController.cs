using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.ApplicationCore.Services.AuthService;

namespace NotesApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
    {
        var result = await _authService.RegisterUser(registerUserRequest);

        return result.ToOk();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _authService.LoginUser(loginRequest);

        if (result.IsSuccess)
        {
           var token = result.Match<JwtToken>(obj => obj, null);
           SetRefreshToken(token);
        }
        
        return result.ToOk();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        var result = await _authService.RefreshToken(refreshToken);
        
        if (result == null)
            return Unauthorized();
        
        SetRefreshToken(result);

        return Ok(result.Token);
    }

    private void SetRefreshToken(JwtToken refreshToken)
    {
        var cookieOption = new CookieOptions()
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };
        
        Response.Cookies.Append("refreshToken", refreshToken.RefreshToken, cookieOption);
    }
}