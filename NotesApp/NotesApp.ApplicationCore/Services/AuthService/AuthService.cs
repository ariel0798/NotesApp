using System.Security.Claims;
using MediatR;
using AutoMapper;
using NotesApp.ApplicationCore.Helper.Interfaces;
using Microsoft.AspNetCore.Http;
using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.ApplicationCore.Models;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.ApplicationCore.Users.Commands;
using NotesApp.ApplicationCore.Users.Queries;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IPasswordHashHelper _passwordHashHelper;
    private readonly IJwtHelper _jwtHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IMediator mediator, IMapper mapper, IPasswordHashHelper passwordHashHelper, IJwtHelper jwtHelper,
        IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _passwordHashHelper = passwordHashHelper;
        _jwtHelper = jwtHelper;
        _httpContextAccessor = contextAccessor;
    }

    public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name); 
    public async Task<string> RegisterUser(RegisterUserRequest userDto)
    {
        _passwordHashHelper.CreatePasswordHash(userDto.Password, out string passwordHash, out string passwordSalt);

        var createUserCommand = _mapper.Map<CreateUserCommand>(userDto);
        
        createUserCommand.PasswordHash = passwordHash;
        createUserCommand.PasswordSalt = passwordSalt;
        
        createUserCommand.TokenCreated = DateTime.Now;
        createUserCommand.TokenExpires = DateTime.Now;


        var user = await _mediator.Send(createUserCommand);
        
        CreateNote(user);
        
        return user.Id;
    }

    public async Task<JwtToken?> LoginUser(LoginRequest userDto)
    {
        var query = new GetUserByEmailQuery() 
            { Email = userDto.Email };
        var user = await  _mediator.Send(query);

        if (user == null)
            return null;

        if (!_passwordHashHelper.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            return null;

        var fullToken = await SetTokenAndRefreshToken(user);
        
        return fullToken;
    }

    public async Task<JwtToken?> RefreshToken(string refreshToken)
    {
        var query = new GetUserByRefreshTokenQuery()
        {
            RefreshToken = refreshToken
        };
        
        var user = await  _mediator.Send(query);
        
        if (user == null)
            return null;

        if (user.TokenExpires < DateTime.Now)
            return null;
        
        var fullToken = await SetTokenAndRefreshToken(user);
        
        return fullToken;
    }

    private async Task<JwtToken> SetTokenAndRefreshToken(User user)
    {
        var token = _jwtHelper.CreateToken(user.Email);

        var fullToken = _jwtHelper.GenerateRefreshToken();
        fullToken.Token = token;

        var setTokenCommand = _mapper.Map<SetUserTokenCommand>(fullToken);
        setTokenCommand.UserId = user.Id;

        await _mediator.Send(setTokenCommand);

        return fullToken;
    }
    
    private async Task<User> CreateNote(User user)
    {
        var note = await _mediator.Send(new CreateNoteCommand());

        var command = new AddUserNoteIdCommand()
        {
            User = user, 
            NoteId = note.Id
        };

        return await _mediator.Send(command);
    }
}