using System.Security.Authentication;
using System.Security.Claims;
using MediatR;
using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.ApplicationCore.Users.Commands;
using NotesApp.ApplicationCore.Users.Queries;
using NotesApp.Domain.Models;
using NotesApp.ApplicationCore.Authentication;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Authentication.Models;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Errors.Messages;

namespace NotesApp.ApplicationCore.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IValidator<RegisterUserRequest> _validatorRegister;

    public AuthService(IMediator mediator, IMapper mapper,
        IHttpContextAccessor contextAccessor, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, IValidator<RegisterUserRequest> validatorRegister)
    {
        _mediator = mediator;
        _mapper = mapper;
        _httpContextAccessor = contextAccessor;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _validatorRegister = validatorRegister;
    }

    public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name); 
    public async Task<Result<bool>> RegisterUser(RegisterUserRequest registerUserRequest)
    {
        var validationResult = await _validatorRegister.ValidateAsync(registerUserRequest);
        if (!validationResult.IsValid)
            return new Result<bool>().CreateValidationException<bool>(validationResult.Errors);

        var userQuery = new GetUserByEmailQuery { Email = registerUserRequest.Email };
        var emailUser = await _mediator.Send(userQuery);
        
        if (emailUser != null)
            return new Result<bool>().CreateException<bool,EmailDuplicatedException>();
        
        _passwordHasher.CreatePasswordHash(registerUserRequest.Password, out string passwordHash, out string passwordSalt);

        var createUserCommand = _mapper.Map<CreateUserCommand>(registerUserRequest);
        
        createUserCommand.PasswordHash = passwordHash;
        createUserCommand.PasswordSalt = passwordSalt;

        var user = await _mediator.Send(createUserCommand);
        
        await CreateNote(user);
        
        return user.Id != null;
    }

    public async Task<Result<JwtToken>> LoginUser(LoginRequest loginRequest)
    {
        var query = new GetUserByEmailQuery() 
            { Email = loginRequest.Email };
        var user = await  _mediator.Send(query);

        if (user == null)
            return new Result<JwtToken>().CreateException<JwtToken,InvalidCredentialException>();

        if (!_passwordHasher.VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            return new Result<JwtToken>().CreateException<JwtToken,InvalidCredentialException>();

        var fullToken = await SetTokenAndRefreshToken(user);
        
        return fullToken;
    }

    public async Task<Result<JwtToken>> RefreshToken(string refreshToken)
    {
        var query = new GetUserByRefreshTokenQuery()
        {
            RefreshToken = refreshToken
        };
        
        var user = await  _mediator.Send(query);
        
        if (user == null)
            return new Result<JwtToken>().CreateException<JwtToken,InvalidCredentialException>();

        if (user.TokenExpires < DateTime.Now)
            return new Result<JwtToken>().CreateException<JwtToken,InvalidCredentialException>();
        
        var fullToken = await SetTokenAndRefreshToken(user);
        
        return fullToken;
    }

    private async Task<JwtToken> SetTokenAndRefreshToken(User user)
    {
        var token = _jwtTokenGenerator.CreateToken(user.Email);

        var fullToken = _jwtTokenGenerator.GenerateRefreshToken();
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