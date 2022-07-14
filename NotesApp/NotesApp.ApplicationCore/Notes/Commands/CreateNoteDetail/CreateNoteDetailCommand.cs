using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Responses;

namespace NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;

public record CreateNoteDetailCommand(
    string Title,
    string Description) : IRequest<Result<GetNoteDetailResponse>>
{
}