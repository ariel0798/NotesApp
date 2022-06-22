using System.Text.Json;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using StackExchange.Redis;
using StackExchange.Redis.KeyspaceIsolation;

namespace NotesApp.Infrastructure.Data.Repositories;

public class NoteRepository :  INoteRepository
{
    private readonly IDatabase _database;

    public NoteRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase().WithKeyPrefix("Note");
    }

    public async Task<Note> Create(Note obj)
    {
        var stringNote = JsonSerializer.Serialize(obj);

        await _database.StringSetAsync(obj.Id, stringNote);
        
        return obj;
    }

    public async Task<Note?> GetById(string id)
    {
        var stringNote = await _database.StringGetAsync(id);

        if(!string.IsNullOrEmpty(stringNote))
        {
            return JsonSerializer.Deserialize<Note>(stringNote.ToString());
        }
        
        return null;
    }

    public async Task Update(Note obj)
    {
        var stringNote = JsonSerializer.Serialize(obj);

        await _database.StringSetAsync(obj.Id, stringNote);
    }

    public async Task Remove(Note obj)
    {
        await _database.StringGetDeleteAsync(obj.Id);
    }
    
    
}