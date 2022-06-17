using MediatR;
using NotesApp.ApplicationCore.Users.Commands;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.CommandHandlers;

public class SetUserTokenCommandHandler : IRequestHandler<SetUserTokenCommand,User>
{
    private readonly IUserRepository _userRepository;

    public SetUserTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> Handle(SetUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);

        user.RefreshToken = request.RefreshToken;
        user.TokenCreated = request.TokenCreated;
        user.TokenExpires = request.TokenExpires;
        
        await _userRepository.Update(user);
        
        return user;
    }
}