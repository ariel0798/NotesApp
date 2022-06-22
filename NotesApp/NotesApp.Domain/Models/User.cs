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
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; } 
    [Searchable(Sortable = true)]
    public string RefreshToken { get;  set; } 
    public DateTime TokenCreated { get;  set; } 
    public DateTime TokenExpires { get;  set; }
    public DateTime UserCreated { get; } = DateTime.Now;
    public string? NoteId { get; set; } = string.Empty;


}