using MediatR;

namespace NotesApp.ApplicationCore.Notes.Queries.GetNoteIdByUserEmail;

public record GetNoteIdByUserEmailQuery() : IRequest<string?>
{
}