using SaltingApi.Models;

namespace SaltingApi.Repository;

public interface IUserCredentialsRepository
{
    Task AddAsync(StoredCredentials storedCredentials);
    Task<StoredCredentials> GetAsync(string user);
}
