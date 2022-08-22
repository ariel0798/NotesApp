using System.Security.Cryptography;
using System.Text.Json;
using NotesApp.ApplicationCore.Authentication.Interfaces;

namespace NotesApp.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    public void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computerHash.SequenceEqual(passwordHash);
    }
   
}