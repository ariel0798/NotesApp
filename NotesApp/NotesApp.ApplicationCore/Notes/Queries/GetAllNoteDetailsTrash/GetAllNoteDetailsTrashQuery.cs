using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;

namespace NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetailsTrash;

public class GetAllNoteDetailsTrashQuery : IRequest<Result<List<GetNoteDetailResponse>>>
{
}