using System;
using System.Text.RegularExpressions;

public static class InputValidator
{
    public static bool IsValidUsername(string username)
    {
        return !string.IsNullOrWhiteSpace(username) &&
               Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,30}$");
    }

    public static bool IsValidEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) &&
               Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}