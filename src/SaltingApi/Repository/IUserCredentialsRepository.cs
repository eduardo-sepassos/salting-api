using Microsoft.EntityFrameworkCore;
using SaltingApi.Models;

namespace SaltingApi.Repository;

public interface IUserCredentialsRepository
{
    Task AddAsync(StoredCredentials storedCredentials);
    Task<StoredCredentials> GetAsync(string user);
}

public class UserCredentialsRepository : IUserCredentialsRepository
{
    private readonly SaltingApiContext _context;

    public UserCredentialsRepository(SaltingApiContext context)
    {
        _context = context;
    }

    public async Task AddAsync(StoredCredentials storedCredentials)
    {
        try
        {
            await _context.AddAsync(storedCredentials);
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            throw new Exception("Unable to add");
        }
    }

    public async Task<StoredCredentials?> GetAsync(string user)
    {
        return await _context.StoredCredentials.FirstOrDefaultAsync(
            x => x.User == user);
    }
}
