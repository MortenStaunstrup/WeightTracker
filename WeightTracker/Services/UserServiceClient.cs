using Core;
using WeightTracker.Services.Interfaces;
using Blazored.LocalStorage;

namespace WeightTracker.Services;

public class UserServiceClient : IUserService
{
    private readonly ILocalStorageService _localStorage;

    public UserServiceClient(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    
    private List<User> users = new List<User>
    {
        new User
        {
            Id = 0,
            FirstName = "Morten",
            LastName = "Nielsen",
            Email = "morten@gmail.com",
            Birthday = new DateOnly(2002, 3, 12),
            Password = "pass",
            Gender = 'M'
        },
        new User
        {
            Id = 1,
            FirstName = "Anna",
            LastName = "Smith",
            Email = "anna.smith@example.com",
            Birthday = new DateOnly(1995, 7, 24),
            Password = "secure123",
            Gender = 'F'
        },
        new User
        {
            Id = 2,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthday = new DateOnly(1988, 11, 5),
            Password = "johnpass",
            Gender = 'M'
        },
        new User
        {
            Id = 3,
            FirstName = "Emily",
            LastName = "Jones",
            Email = "emily.j@example.com",
            Birthday = new DateOnly(1992, 1, 16),
            Password = "emily789",
            Gender = 'F'
        },
        new User
        {
            Id = 4,
            FirstName = "David",
            LastName = "Brown",
            Email = "david.brown@example.com",
            Birthday = new DateOnly(2000, 5, 14),
            Password = "davidpass",
            Gender = 'M'
        }
    };

    public async Task<User?> GetUserLoggedInAsync()
    {
        return await _localStorage.GetItemAsync<User>("user");
    }
    

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = users.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            if (user.Password == password)
            {
                var userToSend = new User()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Birthday = user.Birthday,
                    Gender = user.Gender
                };
                return userToSend;
            }
        }
        return null;
    }

    public async Task<bool> RegisterUserAsync(User user)
    {
        var userExists = users.FirstOrDefault(u => u.Email == user.Email);
        if (userExists != null)
        {
            return false;
        }
        
        users.Add(user);
        return true;
    }
    
    
}