using System.Security.Cryptography;
using System.Text.Json;
using NotesApp.ApplicationCore.Helper.Interfaces;

namespace NotesApp.ApplicationCore.Helper;

public class PasswordHashHelper : IPasswordHashHelper
{
    public void CreatePasswordHash(string password,out string passwordHash, out string passwordSalt)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        passwordHash = Base64Encode(JsonSerializer.Serialize(hash));
        passwordSalt = Base64Encode(JsonSerializer.Serialize(salt));
    }
    private static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
}