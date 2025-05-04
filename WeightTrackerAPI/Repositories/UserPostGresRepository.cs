using Core;
using Npgsql;
using WeightTrackerAPI.Repositories.Interfaces;

namespace WeightTrackerAPI.Repositories;

public class UserPostGresRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserPostGresRepository()
    {
        _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new InvalidOperationException("No connection string configured");
        }
    }
    
    public async Task<User?> LoginAsync(string email, string password)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd =
            new NpgsqlCommand(
                "SELECT id, email, gender, birthday, firstname, lastname FROM users WHERE email = @email AND password = @password",
                conn);
        cmd.Parameters.AddWithValue("email", email);
        cmd.Parameters.AddWithValue("password", password);

        await using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var user = new User()
            {
                Id = reader.GetInt32(0),
                Email = reader.GetString(1),
                Gender = reader.GetChar(2),
                Birthday = DateOnly.FromDateTime(reader.GetDateTime(3)),
                FirstName = reader.GetString(4),
                LastName = reader.GetString(5)
            };
            return user;
        }
        return null;
    }

    public async Task<bool> RegisterUserAsync(User user)
    {
        if (!await UserExists(user.Email))
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            
            await using (var cmd = new NpgsqlCommand("INSERT INTO users (email, password, gender, birthday, firstname, lastname) VALUES (@email, @password, @gender, @birthday, @firstname, @lastname)", conn))
            {
                cmd.Parameters.AddWithValue("email", user.Email);
                cmd.Parameters.AddWithValue("password", user.Password);
                cmd.Parameters.AddWithValue("gender", user.Gender);
                cmd.Parameters.AddWithValue("birthday", user.Birthday);
                cmd.Parameters.AddWithValue("firstname", user.FirstName);
                cmd.Parameters.AddWithValue("lastname", user.LastName);
                await cmd.ExecuteNonQueryAsync();
            }
            return true;
        }
        return false;
    }

    public async Task<bool> UserExists(string email)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd1 = new NpgsqlCommand("SELECT email FROM users WHERE email = @email", conn);
        cmd1.Parameters.AddWithValue("email", email);

        await using var reader = await cmd1.ExecuteReaderAsync();
        if(reader.HasRows)
            return true;
        return false;
    }
    
}