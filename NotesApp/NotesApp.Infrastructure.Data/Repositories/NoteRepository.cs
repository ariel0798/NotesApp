using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using Redis.OM;

namespace NotesApp.Infrastructure.Data.Repositories;

public class NoteRepository : BaseRepository<Note> , INoteRepository
{
    public NoteRepository(RedisConnectionProvider provider) : base(provider)
    {
    }
}