using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserAPI.Attributes;

public class PasswordAttribute : ValidationAttribute
{
    private const string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";

    public override bool IsValid(object value)
    {
        var password = value.ToString();
        var regex = new Regex(pattern);

        return regex.IsMatch(password);
    }

    public override string FormatErrorMessage(string password)
    {
        return $"The {password} field must conform to a minimum criteria of 8 characters at least 1 Capital, 1 Small and 1 digit.";
    }
}