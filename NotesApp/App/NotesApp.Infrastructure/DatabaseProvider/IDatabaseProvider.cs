using System.Data.SqlClient;

namespace NotesApp.Infrastructure.DatabaseProvider;

public interface IDatabaseProvider
{
    SqlConnection GetConnection();
}