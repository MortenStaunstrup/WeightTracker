using System.Net.Http.Json;
using Core;
using WeightTracker.Services.Interfaces;

namespace WeightTracker.Services;

public class WeightServiceServer : IWeightService
{
    private HttpClient _client;
    private readonly string BaseURL = "http://localhost:5010/api/weight";

    public WeightServiceServer(HttpClient client)
    {
        _client = client;
    }
    
    public void AddDailyWeightAsync(Weight weight)
    {
        _client.PostAsJsonAsync($"{BaseURL}/add", weight);
        Console.WriteLine($"Adding daily weight SERVICE {weight.WeightKg}");
    }

    public async Task<List<Weight>?> GetAllWeightsByUserIdAsync(int userId)
    {
        return await _client.GetFromJsonAsync<List<Weight>?>($"{BaseURL}/get/{userId}");
    }

    public async Task<List<Weight>?> GetAllAvgWeightsByUserIdAsync(int userId)
    {
        return await _client.GetFromJsonAsync<List<Weight>?>($"{BaseURL}/getavg/{userId}");
    }

    public async Task<Weight?> GetLatestTrackedWeightByUserIdAsync(int userId)
    {
        return await _client.GetFromJsonAsync<Weight?>($"{BaseURL}/getlatest/{userId}");
    }

    public void UpdateWeight(Weight weight)
    {
        _client.PutAsJsonAsync($"{BaseURL}/update", weight);
    }
}