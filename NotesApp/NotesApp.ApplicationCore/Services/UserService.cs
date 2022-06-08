using MediatR;
using AutoMapper;
using NotesApp.ApplicationCore.Commands.User;
using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Helper.Interfaces;
using NotesApp.ApplicationCore.Interfaces;

namespace NotesApp.ApplicationCore.Services;

public class UserService : IUserService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IPasswordHashHelper _passwordHashHelper;

    public UserService(IMediator mediator, IMapper mapper, IPasswordHashHelper passwordHashHelper)
    {
        _mediator = mediator;
        _mapper = mapper;
        _passwordHashHelper = passwordHashHelper;
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
}