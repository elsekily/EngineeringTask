using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Core;
using UserAPI.Entities.Resources;

public class SecurityService : ISecurityService
{
    private readonly byte[] salt = Encoding.UTF8.GetBytes("MySaltValue12345");
    private readonly int iterations = 1000;
    public string Encrypt(BirthdateAddressCombination birthdateAddressCombination, string password)
    {
        byte[] dataToEncrypt = Encoding.UTF8.GetBytes($"{birthdateAddressCombination.BirthDate}|{birthdateAddressCombination.Address}");

        using (Aes aes = Aes.Create())
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                }

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }

    public BirthdateAddressCombination Decrypt(string encryptedData, string password)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

        using (Aes aes = Aes.Create())
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);

            using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        string decryptedData = streamReader.ReadToEnd();
                        string[] parts = decryptedData.Split('|');
                        BirthdateAddressCombination result = new BirthdateAddressCombination()
                        {
                            BirthDate = DateTime.Parse(parts[0]),
                            Address = parts[1]
                        };

                        return result;
                    }
                }
            }
        }
    }

    public string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        // Hash the password with the salt
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPasswordBytes = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, saltedPasswordBytes, passwordBytes.Length, salt.Length);
        byte[] hashedBytes = new SHA512Managed().ComputeHash(saltedPasswordBytes);
        string hashedPassword = Convert.ToBase64String(hashedBytes);

        // Append the salt to the end of the hash
        string saltString = Convert.ToBase64String(salt);
        return hashedPassword + saltString;
    }
    public bool CheckPassword(string password, string hashedPasswordWithSalt)
    {
        // Extract the hash and salt from the hashed password string
        string hashedPassword = hashedPasswordWithSalt.Substring(0, hashedPasswordWithSalt.Length - 24);
        string saltString = hashedPasswordWithSalt.Substring(hashedPasswordWithSalt.Length - 24);

        // Convert the salt from a string to a byte array
        byte[] salt = Convert.FromBase64String(saltString);

        // Hash the password with the salt and compare to the stored hash
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPasswordBytes = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, saltedPasswordBytes, passwordBytes.Length, salt.Length);
        byte[] hashedBytes = new SHA512Managed().ComputeHash(saltedPasswordBytes);
        string hashedPasswordToCheck = Convert.ToBase64String(hashedBytes);
        return hashedPasswordToCheck == hashedPassword;
    }

}
