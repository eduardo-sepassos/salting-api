using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SaltingApi.Models;

public class StoredCredentials
{
    [JsonIgnore]
    [Key]
    public int Id { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}
