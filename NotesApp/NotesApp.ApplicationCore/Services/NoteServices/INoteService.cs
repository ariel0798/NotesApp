using NotesApp.ApplicationCore.Dtos.Note;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Services.NoteServices;

public interface INoteService
{
    Task<NoteDetail> CreateNoteDetail(CreateNoteDto noteDto);
    Task<NoteDetail?> GetNoteDetailById(string noteDetailId);
}