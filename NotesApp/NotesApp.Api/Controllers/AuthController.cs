using Microsoft.AspNetCore.Mvc;
using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Interfaces;
using NotesApp.ApplicationCore.Models;

namespace NotesApp.Api.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto userDto)
    {
        var result = await _authService.RegisterUser(userDto);
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto userDto)
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