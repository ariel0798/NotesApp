using MediatR;
using NotesApp.ApplicationCore.Users.Queries;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.QueryHandlers;

public class GetUserByRefreshTokenQueryHandler : IRequestHandler<GetUserByRefreshTokenQuery,User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByRefreshTokenQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        //TODO Not reading RefreshToken property fix 
        var user = await _userRepository.FindFirstByCondition(u => u.RefreshToken == request.RefreshToken);

        return new User();
    }
}