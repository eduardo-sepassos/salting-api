namespace SaltingApi.Models;

public class HashedCredentials
{
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
}