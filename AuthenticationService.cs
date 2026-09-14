using System;
using System.Collections.Generic;

public class AuthenticationService
{
    private readonly Dictionary<string, string> users = new();

    // Register a user with a securely hashed password
    public void Register(string username, string password)
    {
        if (!InputValidator.IsValidUsername(username))
            throw new ArgumentException("Invalid username.", nameof(username));

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters long.", nameof(password));

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        users[username] = hashedPassword;
    }

    // Authenticate user by verifying the password
    public bool Authenticate(string username, string password)
    {
        if (!users.ContainsKey(username))
            return false;

        return BCrypt.Net.BCrypt.Verify(password, users[username]);
    }
}