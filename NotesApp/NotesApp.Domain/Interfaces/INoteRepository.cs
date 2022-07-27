using NotesApp.Domain.Models;

namespace NotesApp.Domain.Interfaces;

public interface INoteRepository
{
    Task<Note> Create(Note obj);
    Task<Note?> GetById(string id);
    Task<IQueryable<Note>> GetAll();
    Task Update(Note obj);
}