using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;

namespace NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetails;

public class GetAllNoteDetailsQuery : IRequest<Result<IEnumerable<NoteDetailResponse>>>
{
}