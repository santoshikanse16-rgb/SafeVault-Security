using MySql.Data.MySqlClient;

public class UserRepository
{
    private string connectionString = "YOUR_CONNECTION_STRING";

    public void AddUser(string username, string email)
    {
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