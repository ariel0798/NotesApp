using Microsoft.AspNetCore.Mvc;
using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.ApplicationCore.Models;
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
    public async Task<IActionResult> Register(RegisterUserRequest userDto)
    {
        var result = await _authService.RegisterUser(userDto);
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest userDto)
    {
        var result = await _authService.LoginUser(userDto);

        if (result == null)
            return Unauthorized();
        
        SetRefreshToken(result);

        return Ok(result.Token);
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