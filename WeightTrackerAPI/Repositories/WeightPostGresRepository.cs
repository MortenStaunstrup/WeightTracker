using Core;
using Npgsql;
using WeightTrackerAPI.Repositories.Interfaces;

namespace WeightTrackerAPI.Repositories;

public class WeightPostGresRepository : IWeightRepository
{

    private readonly string _connectionString;

    public WeightPostGresRepository()
    {
        _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new InvalidOperationException("No connection string configured");
        }
    }
    
    public async void AddDailyWeightAsync(Weight weight)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        
        await using (var cmd = new NpgsqlCommand("INSERT INTO weight VALUES (@userid, @weightkg, @weightlbs, @date)", conn))
        {
            cmd.Parameters.AddWithValue("userid", weight.UserId);
            cmd.Parameters.AddWithValue("weightkg", weight.WeightKg);
            cmd.Parameters.AddWithValue("weightlbs", weight.WeightLbs);
            cmd.Parameters.AddWithValue("date", weight.Date);
            var result = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"Rows affected: {result}");
        }
        
    }

    public async Task<List<Weight>?> GetAllWeightsByUserIdAsync(int userId)
    {
        throw new Exception("Not implemented");
    }

    public async Task<List<Weight>?> GetAllAvgWeightsByUserIdAsync(int userId)
    {
        throw new AbandonedMutexException();
    }

    public async Task<Weight?> GetLatestTrackedWeightByUserIdAsync(int userId)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM weight WHERE userid = @userid ORDER BY date DESC LIMIT 1", conn);
        cmd.Parameters.AddWithValue("userid", userId);
        
        await using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var weight = new Weight()
            {
                UserId = reader.GetInt32(0),
                WeightKg = reader.GetDouble(1),
                WeightLbs = reader.GetDouble(2),
                Date = DateOnly.FromDateTime(reader.GetDateTime(3))
            };
            return weight;
        }
        return null;
    }

    public async void UpdateTodaysWeightAsync(Weight weight)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        
        await using (var cmd = new NpgsqlCommand("UPDATE weight SET weightkg = @weightkg , weightlbs = @weightlbs WHERE userid = @userid AND date = @date", conn))
        {
            cmd.Parameters.AddWithValue("userid", weight.UserId);
            cmd.Parameters.AddWithValue("weightkg", weight.WeightKg);
            cmd.Parameters.AddWithValue("weightlbs", weight.WeightLbs);
            cmd.Parameters.AddWithValue("date", weight.Date);
            var result = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"Rows affected: {result}");
        }
    }
}