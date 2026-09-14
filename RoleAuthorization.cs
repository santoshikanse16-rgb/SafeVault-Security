using System;
using System.Collections.Generic;

public class RoleAuthorization
{
    private readonly Dictionary<string, string> userRoles = new();

    // Assign a role to a user
    public void AssignRole(string username, string role)
    {
        if (!InputValidator.IsValidUsername(username))
            throw new ArgumentException("Invalid username.", nameof(username));

        if (role != "admin" && role != "user")
            throw new ArgumentException("Invalid role.");

        userRoles[username] = role;
    }

    // Check whether a user has permission
    public bool CanAccessAdminDashboard(string username)
    {
        return userRoles.ContainsKey(username) &&
               userRoles[username] == "admin";
    }
}