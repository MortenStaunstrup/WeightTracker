using Core;

namespace WeightTrackerAPI.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> LoginAsync(string email, string password);
    Task<bool> RegisterUserAsync(User user);
    Task<bool> UserExists(string email);
}