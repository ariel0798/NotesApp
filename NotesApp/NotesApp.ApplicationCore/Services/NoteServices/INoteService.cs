using NotesApp.ApplicationCore.Dtos.Note;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Services.NoteServices;

public interface INoteService
{
    Task<NoteDetail> CreateNote(CreateNoteDto noteDto);
    Task<NoteDetail?> ReadNote(string noteDetailId);
}