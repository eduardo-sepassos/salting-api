using System.ComponentModel.DataAnnotations;

namespace SaltingApi.Models;

public class UserCredentials
{
    public string User { get; set; }
    public string Password { get; set; }
}
