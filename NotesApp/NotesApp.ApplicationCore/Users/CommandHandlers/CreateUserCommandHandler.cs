using AutoMapper;
using MediatR;
using NotesApp.ApplicationCore.Users.Commands;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.CommandHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        
        user.SetPassword(request.PasswordHash, request.PasswordSalt);
        user.AddEmail(request.Email);
       await  _userRepository.Create(user);
       return user;
    }
}