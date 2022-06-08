using MediatR;
using AutoMapper;
using System.Text.Json;
using System.Security.Cryptography;
using NotesApp.ApplicationCore.Commands.User;
using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Interfaces;

namespace NotesApp.ApplicationCore.Services;

public class UserService : IUserService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<string> RegisterUser(RegisterUserDto userDto)
    {
        CreatePasswordHash(userDto.Password, out string passwordHash, out string passwordSalt);

        var createUserCommand = _mapper.Map<CreateUserCommand>(userDto);
        
        createUserCommand.PasswordHash = passwordHash;
        createUserCommand.PasswordSalt = passwordSalt;
        
        createUserCommand.TokenCreated = DateTime.Now;
        createUserCommand.TokenExpires = DateTime.Now;
        createUserCommand.NoteId = "";

        var user = await _mediator.Send(createUserCommand);
        return user.Id;
    }
    
    private void CreatePasswordHash(string password,out string passwordHash, out string passwordSalt)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        passwordHash = Base64Encode(JsonSerializer.Serialize(hash));
        passwordSalt = Base64Encode(JsonSerializer.Serialize(salt));
    }
    private static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
}