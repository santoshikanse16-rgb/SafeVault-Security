using System;
using System.Collections.Generic;

public class AuthenticationService
{
    private readonly Dictionary<string, string> users = new();

    // Register a user with a securely hashed password
    public void Register(string username, string password)
    {
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