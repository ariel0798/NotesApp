using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;

namespace NotesApp.ApplicationCore.Notes.Queries.GetNoteDetailById;

public record GetNoteDetailByIdQuery(string NoteDetailId) : IRequest<Result<GetNoteDetailResponse>>
{
    
}