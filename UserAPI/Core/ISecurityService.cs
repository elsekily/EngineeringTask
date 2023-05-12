using UserAPI.Entities.Resources;

namespace UserAPI.Core;

public interface ISecurityService
{
    string Encrypt(BirthdateAddressCombination birthdateAddressCombination, string password);
    BirthdateAddressCombination Decrypt(string encryptedData, string password);
    string HashPassword(string password);
    bool CheckPassword(string password, string hashedPasswordWithSalt);
}