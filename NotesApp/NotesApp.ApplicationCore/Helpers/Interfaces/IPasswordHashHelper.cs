namespace NotesApp.ApplicationCore.Helpers.Interfaces;

public interface IPasswordHashHelper
{
    void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt);
    bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt);
}