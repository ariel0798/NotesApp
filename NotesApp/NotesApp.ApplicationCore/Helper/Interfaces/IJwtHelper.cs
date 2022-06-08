using NotesApp.ApplicationCore.Dtos.User;

namespace NotesApp.ApplicationCore.Helper.Interfaces;

public interface IJwtHelper
{
    string CreateToken(string email);
}