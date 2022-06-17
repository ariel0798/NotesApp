using MediatR;
using NotesApp.ApplicationCore.Users.Commands;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.CommandHandlers;

public class AddUserNoteIdCommandHandler : IRequestHandler<AddUserNoteIdCommand,User>
{
    private readonly IUserRepository _userRepository;

    public AddUserNoteIdCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> Handle(AddUserNoteIdCommand request, CancellationToken cancellationToken)
    {
        request.User.NoteId = request.NoteId;

        await _userRepository.Update(request.User);

        return request.User;
    }
}