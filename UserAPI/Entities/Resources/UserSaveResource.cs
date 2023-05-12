using System.ComponentModel.DataAnnotations;
using UserAPI.Attributes;

namespace UserAPI.Entities.Resources;
public class UserSaveResource
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string FatherName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string FamilyName { get; set; } = string.Empty;
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    [MaxLength(255)]
    public string Address { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [Password]
    public string Password { get; set; } = string.Empty;
}