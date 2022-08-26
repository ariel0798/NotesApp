using NotesApp.Domain.Models;

namespace NotesApp.Domain.Interfaces;

public interface INoteRepository : IGenericRepository<Note>
{
    Task<Note> GetNoteByUserId(int userId);
    Task<NoteDetail> GetNoteDetailByNoteDetailIdAndUserId(int noteDetailId, int userId);
    Task<IEnumerable<NoteDetail>> GetAllNoteDetailsByUserId(int userId);
    Task<NoteDetail> AddNoteDetail(NoteDetail noteDetail);
    void UpdateNoteDetail(NoteDetail noteDetail);
    Task<bool> DeleteNoteDetail(int noteDetailId, int userId);
    Task DeleteNoteDetailById(int noteDetailId);
    Task<IEnumerable<NoteDetail>> GetExpiredNoteDetails(int days);
}