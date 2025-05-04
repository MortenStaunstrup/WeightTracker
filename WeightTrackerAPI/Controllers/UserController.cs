using Core;
using Microsoft.AspNetCore.Mvc;
using WeightTrackerAPI.Repositories.Interfaces;

namespace WeightTrackerAPI.Controllers;

[Route("api/users")]
public class UserController : ControllerBase
{
    IUserRepository _UserRepository;

    public UserController(IUserRepository repository)
    {
        _UserRepository = repository;
    }

    [HttpGet]
    [Route("login/{email}/{password}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    public async Task<IActionResult> LoginAsync(string email, string password)
    {
        var result = await _UserRepository.LoginAsync(email, password);
        if (result == null)
        {
            return Ok(new User());
        }
        return Ok(result);
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<IActionResult> RegisterUserAsync([FromBody] User user)
    {
        if (!await _UserRepository.RegisterUserAsync(user))
        {
            return Ok(false);
        }
        return Ok(true);
    }
}