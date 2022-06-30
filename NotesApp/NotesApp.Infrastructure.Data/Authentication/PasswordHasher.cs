using System.Security.Cryptography;
using System.Text.Json;
using NotesApp.ApplicationCore.Authentication;

namespace NotesApp.Infrastructure.Data.Authentication;

public class PasswordHasher : IPasswordHasher
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
        var salt = JsonSerializer.Deserialize<byte[]>(Base64Decode(passwordSalt));
        var hash = JsonSerializer.Deserialize<byte[]>(Base64Decode(passwordHash));
        
        using var hmac = new HMACSHA512(salt);
        var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computerHash.SequenceEqual(hash);
    }
    private static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    private static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}