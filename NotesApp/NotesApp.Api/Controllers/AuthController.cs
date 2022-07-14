using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Authentication.Commands.Login;
using NotesApp.ApplicationCore.Authentication.Commands.RefreshToken;
using NotesApp.ApplicationCore.Authentication.Commands.Register;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;

namespace NotesApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public AuthController( IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost(ApiRoutes.Authentication.Registration)]
    public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
    {
        var command = _mapper.Map<RegisterCommand>(registerUserRequest);

        var result = await _mediator.Send(command);

        return result.ToOk();
    }
    
    [HttpPost(ApiRoutes.Authentication.Login)]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var command = _mapper.Map<LoginCommand>(loginRequest);
        
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
           var token = result.Match<JwtToken>(obj => obj, null);
           SetRefreshToken(token);
        }
        
        var resultToken = result.Map<string>(obj => obj.Token);
        return resultToken.ToOk();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        
        var command = new RefreshTokenCommand(refreshToken);
        
        var result = await _mediator.Send(command);
        
        if(result.IsSuccess)
        {
            var token = result.Match<JwtToken>(obj => obj, null);
            SetRefreshToken(token);
        }

        var resultToken = result.Map<string>(obj => obj.Token);
        return resultToken.ToOk();
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