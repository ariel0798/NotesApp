using MediatR;
using LanguageExt.Common;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;

namespace NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;

public record CreateNoteDetailCommand(
    string Title,
    string Description) : IRequest<Result<NoteDetailResponse>>
{
}