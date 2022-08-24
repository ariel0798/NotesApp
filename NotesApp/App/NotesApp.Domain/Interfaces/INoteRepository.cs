using NotesApp.Domain.Models;

namespace NotesApp.Domain.Interfaces;

public interface INoteRepository : IGenericRepository<Note>
{
    Task<Note> GetNoteByUserId(int userId);
    Task<NoteDetail> GetNoteDetailByNoteDetailId(int noteDetailId);
    Task<IEnumerable<NoteDetail>> GetAllNoteDetailsByUserId(int userId);
    Task<NoteDetail> AddNoteDetail(NoteDetail noteDetail);
    void UpdateNoteDetail(NoteDetail noteDetail);
    Task<bool> DeleteNoteDetail(int noteDetailId);
}