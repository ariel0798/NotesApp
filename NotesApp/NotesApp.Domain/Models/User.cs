using Redis.OM.Modeling;

namespace NotesApp.Domain.Models;

[Document(Prefixes = new []{"User"})]
public class User
{
    [RedisIdField]
    public string Id{ get; set; }
    
    public string Name { get; set; }
    public string LastName { get; set; }
    
    [Searchable(Sortable = true)]
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; } 
    [Searchable(Sortable = true)]
    public string RefreshToken { get; private set; } 
    public DateTime TokenCreated { get; private set; } 
    public DateTime TokenExpires { get; private set; }
    public DateTime UserCreated { get; } = DateTime.Now;
    public string? NoteId { get; private set; }

    public void AddToken(string refreshToken, DateTime created, DateTime expires)
    {
        RefreshToken = refreshToken;
        TokenCreated = created;
        TokenExpires = expires;
    }

    public void AddEmail(string email)
    {
        Email = email;
    }
    public void SetPassword(string passwordHash, string passwordSalt)
    {
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    public void AddNoteId(string noteId)
    {
        NoteId = noteId;
    }
}