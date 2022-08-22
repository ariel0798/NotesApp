namespace NotesApp.Domain.Models;

public class User
{
    public User()
    {
        Notes = new HashSet<Note>();
    }

    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime? TokenCreated { get; set; }
    public DateTime? TokenExpires { get; set; }
    public DateTime UserCreated { get; set; }  = DateTime.Now;

    public virtual ICollection<Note> Notes { get; set; }
}