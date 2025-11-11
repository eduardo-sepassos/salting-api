using SaltingApi.Models;

namespace SaltingApi.Handlers;

public interface IPasswordHandler
{
    HashedCredentials CreateCredentials(string password);
    bool VerifyPassword(string password, HashedCredentials credentials);
}
