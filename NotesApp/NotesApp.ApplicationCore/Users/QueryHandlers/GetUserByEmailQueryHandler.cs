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
        request.Email = request.Email.ToLower();
        var users = await _userRepository.GetAll();

        foreach (var user in users)
        {
            if (user.Email.Equals(request.Email))
                    return user;
        }
        return null;
    }
}