
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Services.NoteServices;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private const int DaysToBeRemove = 30;
    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task RemoveDeletedNoteDetails()
    {
        var deleteNoteDetailDictionary = await GetListOfDelete();
        
        foreach (var noteDetailDictionary in deleteNoteDetailDictionary)
        {
            await DeleteNoteDetailsFromNote(noteDetailDictionary.Key, noteDetailDictionary.Value);
        }
    }

    private async Task DeleteNoteDetailsFromNote(string id, List<string> noteDetailIds)
    {
        var note = await _noteRepository.GetById(id);

        foreach (var noteDetailId in noteDetailIds)
        {
            var noteDetail = note.NoteDetails.FirstOrDefault(n => n.NoteDetailId.ToString() == noteDetailId);
            note.NoteDetails.Remove(noteDetail);
        }
        await _noteRepository.Update(note);
    }
    private async Task<Dictionary<string, List<string>>> GetListOfDelete()
    {
        var deleteNoteDetailDictionary = new Dictionary<string, List<string>>();
        var notes = await _noteRepository.GetAll();
        
        foreach (var note in notes)
        {
            var removableNotes = note.NoteDetails
                .Where(d => d.IsDeleted == true && GetDaysDeleted(d.NoteDeleted) > DaysToBeRemove);

            if (removableNotes.Any())
            {
                deleteNoteDetailDictionary.Add(note.Id,removableNotes.Select(x => x.NoteDetailId).ToList());
            }
        }

        return deleteNoteDetailDictionary;
    }

    private int GetDaysDeleted(DateTime? deletedDate)
    {
        if (deletedDate == null)
            return default(int);
        
        return (DateTime.Now.Date - deletedDate.Value.Date).Days;
    }
}