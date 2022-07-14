using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;

namespace NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetails;

public class GetAllNoteDetailsQuery : IRequest<Result<List<GetNoteDetailResponse>>>
{
}