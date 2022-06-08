using Microsoft.AspNetCore.Mvc;
using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Interfaces;

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
}