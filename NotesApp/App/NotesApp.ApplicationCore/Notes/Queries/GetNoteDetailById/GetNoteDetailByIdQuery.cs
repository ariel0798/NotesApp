using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;

namespace NotesApp.ApplicationCore.Notes.Queries.GetNoteDetailById;

public record GetNoteDetailByIdQuery(
    string NoteDetailId): IRequest<Result<NoteDetailResponse>>
{
}