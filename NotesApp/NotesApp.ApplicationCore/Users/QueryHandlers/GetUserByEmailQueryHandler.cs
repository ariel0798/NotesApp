using MediatR;
using NotesApp.ApplicationCore.Users.Queries;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.QueryHandlers;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery,User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.FindFirstByCondition(u => u.Email == request.Email);
    }
}