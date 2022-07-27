using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using StackExchange.Redis;
using StackExchange.Redis.KeyspaceIsolation;

namespace NotesApp.Infrastructure.Data.Repositories;

public class NoteRepository :  INoteRepository
{
    private const string Prefix = "Note";
    private readonly IDatabase _database;
    private readonly RedisKey[] _keys;
    public NoteRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase().WithKeyPrefix(Prefix);
        var endPoint = redis.GetEndPoints().First();
        _keys = redis.GetServer(endPoint).Keys(pattern: Prefix + ":*").ToArray();
    }

    public async Task<Note> Create(Note obj)
    {
        var stringNote = JsonSerializer.Serialize(obj);

        await _database.StringSetAsync(AppendColon(obj.Id), stringNote);
        
        return obj;
    }

    public async Task<Note?> GetById(string id)
    {
        var stringNote = await _database.StringGetAsync(AppendColon(id));

        if(!string.IsNullOrEmpty(stringNote))
        {
            return DeserializeNote(stringNote);
        }
        
        return null;
    }

    public async Task<IQueryable<Note>> GetAll()
    {
        var noteList = new List<Note>();

        foreach (var key in _keys)
        {
            var keyString = key.ToString().Replace(Prefix,"");
            var stringNote = await _database.StringGetAsync(keyString);
            noteList.Add(DeserializeNote(stringNote));
        }

        return noteList.AsQueryable();
    }

    public async Task Update(Note obj)
    {
        var stringNote = JsonSerializer.Serialize(obj);

        await _database.StringSetAsync(AppendColon(obj.Id), stringNote);
    }

    public async Task Remove(Note obj)
    {
        await _database.StringGetDeleteAsync(obj.Id);
    }

    private string AppendColon(string id)
    {
        return ":" + id;
    }

    private Note? DeserializeNote(string stringNote)
    {
        return JsonSerializer.Deserialize<Note>(stringNote.ToString());
    }
}