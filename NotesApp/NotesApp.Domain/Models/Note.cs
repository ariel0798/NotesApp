using Redis.OM.Modeling;

namespace NotesApp.Domain.Models;

[Document(Prefixes = new []{"Note"})]
public class Note
{
    public string Id { get; set; } = Ulid.NewUlid().ToString();
    public List<NoteDetail> NoteDetails { get; set; }
}