using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;
using UserAPI.Attributes;

namespace UserAPI.Entities.Resources;

public class ResetPasswordUserResource
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string OldPassword { get; set; } = string.Empty;
    [Required]
    [Password]
    public string NewPassword { get; set; } = string.Empty;
}