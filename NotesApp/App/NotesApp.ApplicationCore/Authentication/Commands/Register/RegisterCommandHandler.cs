using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Authentication.Interfaces;
using NotesApp.ApplicationCore.Contracts.User.Responses;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand,Result<UserResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<RegisterCommand> _validator;
    private readonly IPasswordHasher _passwordHasher;
    
    public RegisterCommandHandler(IValidator<RegisterCommand> validator, IUnitOfWork unitOfWork, 
        IMapper mapper, IPasswordHasher passwordHasher)
    {
        _validator = validator;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }
    public async Task<Result<UserResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request,cancellationToken);
        
        if (!validationResult.IsValid)
            return new Result<UserResponse>(new ValidationException(validationResult.Errors));

        if (await GetUserByEmail(request.Email) != null)
            return new Result<UserResponse>(ExceptionFactory.EmailDuplicatedException);
        
        _passwordHasher.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var user = _mapper.Map<User>(request);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _unitOfWork.Users.Add(user);
        var note = new Note()
        {
            UserId = user.UserId,
            User = user
        };
        
        await _unitOfWork.Notes.Add(note);
        await _unitOfWork.Save();

        return _mapper.Map<UserResponse>(user);
    }
    
    private async Task<User?> GetUserByEmail(string email)
    {
       return  await _unitOfWork.Users.GetUserByEmail(email);
    }
}