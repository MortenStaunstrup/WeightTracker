using Core;
using Microsoft.AspNetCore.Mvc;
using WeightTrackerAPI.Repositories.Interfaces;

namespace WeightTrackerAPI.Controllers;

[ApiController]
[Route("api/weight")]
public class WeightController : ControllerBase
{
    private readonly IWeightRepository _weightRepository;
    
    public WeightController(IWeightRepository weightRepository)
    {
        _weightRepository = weightRepository;
    }
    
    [HttpPost]
    [Route("add")]
    public void AddDailyWeightAsync(Weight weight)
    {
        _weightRepository.AddDailyWeightAsync(weight);
        Console.WriteLine($"Adding daily weight CONTROLLER {weight.WeightKg}");
    }

    [HttpGet]
    [Route("get/{userId:int}")]
    public async Task<List<Weight>?> GetAllWeightsByUserIdAsync(int userId)
    {
        return await _weightRepository.GetAllWeightsByUserIdAsync(userId);
    }

    [HttpGet]
    [Route("getavg/{userId:int}")]
    public async Task<List<Weight>?> GetAllAvgWeightsByUserIdAsync(int userId)
    {
        return await _weightRepository.GetAllAvgWeightsByUserIdAsync(userId);
    }

    [HttpGet]
    [Route("getlatest/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Weight))]
    public async Task<IActionResult> GetLatestTrackedWeightByUserIdAsync(int userId)
    {
        var result = await _weightRepository.GetLatestTrackedWeightByUserIdAsync(userId);
        if (result == null)
        {
            return Ok(new Weight());
        }
        return Ok(result);
    }

    [HttpPut]
    [Route("update")]
    public void UpdateWeightAsync(Weight weight)
    {
        _weightRepository.UpdateTodaysWeightAsync(weight);
    }
    
}