using AutoMapper;
using MediatR;
using NotesApp.ApplicationCore.Commands.User;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Handlers.User.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Domain.Models.User>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<Domain.Models.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Domain.Models.User>(request);
       await  _userRepository.Create(user);
       return user;
    }
}