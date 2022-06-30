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
        var users = await _userRepository.GetAll();

        foreach (var user in users)
        {
            if (user.RefreshToken != null)
            {
                if (user.RefreshToken.Equals(request.RefreshToken))
                    return user;
            }
            
        }
        
        return null;
    }
}