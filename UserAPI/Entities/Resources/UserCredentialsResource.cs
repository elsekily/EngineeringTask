using System.ComponentModel.DataAnnotations;

namespace UserAPI.Entities.Resources;

public class UserCredentialsResource
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}