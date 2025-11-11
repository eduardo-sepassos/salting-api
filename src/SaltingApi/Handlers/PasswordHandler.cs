using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SaltingApi.Models;
using System.Security.Cryptography;

namespace SaltingApi.Handlers;

public class PasswordHandler  : IPasswordHandler
{
    private const int BYTES = 16;

    private const int ITERATION = 1000;

    public HashedCredentials CreateCredentials(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(BYTES);
        string saltBase64 = Convert.ToBase64String(salt);

        var key = KeyDerivation.Pbkdf2(password,
                                       salt,
                                       KeyDerivationPrf.HMACSHA256,
                                       ITERATION,
                                       BYTES);

        var base64Key = Convert.ToBase64String(key);

        return new HashedCredentials
        {
            PasswordHash = base64Key,
            Salt = saltBase64
        };
    }
    public bool VerifyPassword(string password, HashedCredentials credentials)
    {
        var salt = Convert.FromBase64String(credentials.Salt);

        var result = KeyDerivation.Pbkdf2(password,
                                          salt,
                                          KeyDerivationPrf.HMACSHA256,
                                          ITERATION,
                                          BYTES);

        var base64Result = Convert.ToBase64String(result);

        return string.Equals(base64Result, credentials.PasswordHash);
    }

}
