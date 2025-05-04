using System.Net.Http.Json;
using Blazored.LocalStorage;
using Core;
using WeightTracker.Services.Interfaces;

namespace WeightTracker.Services;

public class UserServiceServer : IUserService
{
    private ILocalStorageService _storage;
    private HttpClient _client;
    private string BaseURL = "http://localhost:5010/api/users";

    public UserServiceServer(ILocalStorageService storage, HttpClient client)
    {
        _storage = storage;
        _client = client;
    }
    
    public async Task<User?> GetUserLoggedInAsync()
    {
        return await _storage.GetItemAsync<User>("user");
    }


    public async Task<User> LoginAsync(string email, string password)
    {
        return await _client.GetFromJsonAsync<User>($"{BaseURL}/login/{email}/{password}");
    }

    public async Task<bool> RegisterUserAsync(User user)
    {
        var response = await _client.PostAsJsonAsync($"{BaseURL}/register", user);
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<bool>();
    }
}