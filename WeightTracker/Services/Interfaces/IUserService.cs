using Core;

namespace WeightTracker.Services.Interfaces;


public interface IUserService
{
    Task<User?> GetUserLoggedInAsync();
    Task<User?> LoginAsync(string email, string password);
    Task<bool> RegisterUserAsync(User user);
}