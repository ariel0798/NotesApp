using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
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
    public bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
    {
        using var hmac = new HMACSHA512(Base64Decode(passwordSalt));
        var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computerHash.SequenceEqual(Base64Decode(passwordHash));
    }
    private static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    private static byte[] Base64Decode(string base64Text)
    {
        var valueBytes = Convert.FromBase64String(base64Text);
        return valueBytes;
    }
}