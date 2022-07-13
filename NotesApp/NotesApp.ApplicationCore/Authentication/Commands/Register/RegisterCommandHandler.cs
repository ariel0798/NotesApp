using AutoMapper;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.Domain.Errors.Exceptions;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand,Result<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly INoteRepository _noteRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper, INoteRepository noteRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _noteRepository = noteRepository;
    }
    
    public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if( await GetUserByEmail(request.Email) != null)
            return new Result<bool>().CreateException<bool,EmailDuplicatedException>();
        
        _passwordHasher.CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);

        var user = _mapper.Map<User>(request);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _userRepository.Create(user);

        await CreateNote(user);
        
        return user.Id != null;
    }

    private async Task<User?> GetUserByEmail(string email)
    {
        var users = await _userRepository.GetAll();

        foreach (var user in users)
        {
            if (user.Email.Equals(email))
                return user;
        }
        return null;
    }
        
    private async Task CreateNote(User user)
    {
        var note = new Note()
        {
            NoteDetails = new List<NoteDetail>()
        };

        await _noteRepository.Create(note);

        user.NoteId = note.Id;

       await _userRepository.Update(user);
    }
}