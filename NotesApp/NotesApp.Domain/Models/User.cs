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
    public string Email { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; } 
    public DateTime TokenExpires { get; set; }
    public DateTime UserCreated { get; set; } = DateTime.Now;
    public string NoteId { get; set; } 
}