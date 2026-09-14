using MySql.Data.MySqlClient;
using System;

public class UserRepository
{
    private string connectionString = "YOUR_CONNECTION_STRING";

    public void AddUser(string username, string email)
    {
        if (!InputValidator.IsValidUsername(username))
            throw new ArgumentException("Invalid username.", nameof(username));

        if (!InputValidator.IsValidEmail(email))
            throw new ArgumentException("Invalid email.", nameof(email));

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Users (Username, Email) VALUES (@username, @email)";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@email", email);

                command.ExecuteNonQuery();
            }
        }
    }
}