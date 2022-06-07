using System.Reflection.Metadata;
using Redis.OM.Modeling;

namespace NotesApp.Domain.Models;

[Document(Prefixes = new []{"Note"})]
public class Note
{
    [RedisIdField]
    public string Id{ get; set; }
    public List<NoteDetail> Notes { get; set; }
}