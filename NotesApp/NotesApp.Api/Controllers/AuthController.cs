using Microsoft.AspNetCore.Mvc;
using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Interfaces;
using NotesApp.ApplicationCore.Models;

namespace NotesApp.Api.Controllers;

public class AuthController : Controller
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto userDto)
    {
        var result = await _userService.RegisterUser(userDto);
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto userDto)
    {
        var result = await _userService.LoginUser(userDto);

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