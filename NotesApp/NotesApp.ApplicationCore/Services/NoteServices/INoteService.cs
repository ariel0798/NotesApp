using NotesApp.ApplicationCore.Contracts.Note.Requests;
using NotesApp.ApplicationCore.Contracts.Note.Responses;

namespace NotesApp.ApplicationCore.Services.NoteServices;

public interface INoteService
{
    Task<GetNoteDetailResponse> CreateNoteDetail(CreateNoteRequest noteDto);
    Task<GetNoteDetailResponse?> GetNoteDetailById(string noteDetailId);
    Task<List<GetNoteDetailResponse>> GetAllNoteDetails();
    Task<List<GetNoteDetailResponse>> GetAllNoteDetailsTrash();
    Task<GetNoteDetailResponse> UpdateNoteDetail(UpdateNoteDetailRequest noteDetailDto);
    Task<bool> RecoverNoteDetail(string noteDetailId);
    Task<bool> SoftDeleteNoteDetail(string noteDetailId);
    Task<bool> DeleteNoteDetail(string noteDetailId);
}