using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Users.Commands;

public class AddUserNoteIdCommand : IRequest<User>
{
    public User User { get; set; }
    public string NoteId { get; set; }
}