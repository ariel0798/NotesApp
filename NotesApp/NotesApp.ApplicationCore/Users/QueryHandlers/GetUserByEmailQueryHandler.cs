using MediatR;
using NotesApp.ApplicationCore.Users.Queries;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Users.QueryHandlers;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery,Domain.Models.User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Domain.Models.User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.FindFirstByCondition(u => u.Email == request.Email);
    }
}