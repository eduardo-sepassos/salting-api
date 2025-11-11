using Microsoft.EntityFrameworkCore;
using SaltingApi.Models;

namespace SaltingApi.Repository.Context;

public class SaltingApiContext : DbContext
{
    public SaltingApiContext(DbContextOptions<SaltingApiContext> options) : base(options)
    {

    }

    public DbSet<StoredCredentials> StoredCredentials { get; set; }
}
