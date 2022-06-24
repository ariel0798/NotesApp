using NotesApp.ApplicationCore.Dtos.Note;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Services.NoteServices;

public interface INoteService
{
    Task<GetNoteDetailDto> CreateNoteDetail(CreateNoteDto noteDto);
    Task<GetNoteDetailDto?> GetNoteDetailById(string noteDetailId);
    Task<List<GetNoteDetailDto>> GetAllNoteDetails();
    Task<List<GetNoteDetailDto>> GetAllNoteDetailsTrash();
    Task<GetNoteDetailDto> UpdateNoteDetail(UpdateNoteDetailDto noteDetailDto);
    Task<bool> SoftDeleteNoteDetail(string noteDetailId);
    Task<bool> DeleteNoteDetail(string noteDetailId);
}