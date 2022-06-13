using MediatR;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands;

public class CreateNoteCommand : IRequest<Note>
{
}