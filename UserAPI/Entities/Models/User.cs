namespace UserAPI.Entities.Models;
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string EncryptedData { get; set; } = string.Empty;

}