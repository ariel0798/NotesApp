using MediatR;
using AutoMapper;
using NotesApp.ApplicationCore.Commands.User;
using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Helper.Interfaces;
using NotesApp.ApplicationCore.Interfaces;
using NotesApp.ApplicationCore.Models;
using NotesApp.ApplicationCore.Queries.User;

namespace NotesApp.ApplicationCore.Services;

public class AuthService : IAuthService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IPasswordHashHelper _passwordHashHelper;
    private readonly IJwtHelper _jwtHelper;

    public AuthService(IMediator mediator, IMapper mapper, IPasswordHashHelper passwordHashHelper, IJwtHelper jwtHelper)
    {
        _mediator = mediator;
        _mapper = mapper;
        _passwordHashHelper = passwordHashHelper;
        _jwtHelper = jwtHelper;
    }

    public async Task<string> RegisterUser(RegisterUserDto userDto)
    {
        _passwordHashHelper.CreatePasswordHash(userDto.Password, out string passwordHash, out string passwordSalt);

        var createUserCommand = _mapper.Map<CreateUserCommand>(userDto);
        
        createUserCommand.PasswordHash = passwordHash;
        createUserCommand.PasswordSalt = passwordSalt;
        
        createUserCommand.TokenCreated = DateTime.Now;
        createUserCommand.TokenExpires = DateTime.Now;
        createUserCommand.NoteId = "";

        var user = await _mediator.Send(createUserCommand);
        return user.Id;
    }

    public async Task<JwtToken?> LoginUser(LoginUserDto userDto)
    {
        var query = new GetUserByEmailQuery() 
            { Email = userDto.Email };
        var user = await  _mediator.Send(query);

        if (user == null)
            return null;

        if (!_passwordHashHelper.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            return null;

        var token = _jwtHelper.CreateToken(user.Email);

        var refreshToken = _jwtHelper.GenerateRefreshToken();
        refreshToken.Token = token;
        
        return refreshToken;
    }
}