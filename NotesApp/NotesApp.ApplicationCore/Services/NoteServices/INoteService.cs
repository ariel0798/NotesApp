using LanguageExt.Common;
using NotesApp.ApplicationCore.Contracts.Note.Requests;
using NotesApp.ApplicationCore.Contracts.Note.Responses;

namespace NotesApp.ApplicationCore.Services.NoteServices;

public interface INoteService
{
    Task<Result<GetNoteDetailResponse>> CreateNoteDetail(CreateNoteRequest createNoteRequest);
    Task<Result<GetNoteDetailResponse>> GetNoteDetailById(string noteDetailId);
    Task<Result<List<GetNoteDetailResponse>>> GetAllNoteDetails();
    Task<Result<List<GetNoteDetailResponse>>> GetAllNoteDetailsTrash();
    Task<Result<GetNoteDetailResponse>> UpdateNoteDetail(UpdateNoteDetailRequest updateNoteDetailRequest);
    Task<Result<bool>> RecoverNoteDetail(string noteDetailId);
    Task<Result<bool>> SoftDeleteNoteDetail(string noteDetailId);
    Task<Result<bool>> DeleteNoteDetail(string noteDetailId);
}