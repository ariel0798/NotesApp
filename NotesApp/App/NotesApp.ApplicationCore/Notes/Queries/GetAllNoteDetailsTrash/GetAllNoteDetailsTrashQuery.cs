using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;

namespace NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetailsTrash;

public class GetAllNoteDetailsTrashQuery :  IRequest<Result<IEnumerable<NoteDetailResponse>>>
{
    
}