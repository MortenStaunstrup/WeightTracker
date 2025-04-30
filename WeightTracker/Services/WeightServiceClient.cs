using Core;
using WeightTracker.Services.Interfaces;

namespace WeightTracker.Services;

public class WeightServiceClient : IWeightService
{
    public List<Weight> allweights = new List<Weight>
    {
        new Weight { Date = new DateOnly(2025, 04, 1), WeightKg = 75, WeightLbs = 165, UserId = 0 },
        new Weight { Date = new DateOnly(2025, 04, 2), WeightKg = 73, WeightLbs = 160, UserId = 0 },
        new Weight { Date = new DateOnly(2025, 04, 3), WeightKg = 77, WeightLbs = 169, UserId = 0 },
        
        new Weight { Date = new DateOnly(2025, 04, 1), WeightKg = 99, WeightLbs = 218, UserId = 1 },
        new Weight { Date = new DateOnly(2025, 04, 2), WeightKg = 98, WeightLbs = 216, UserId = 1 },
        
        new Weight { Date = new DateOnly(2025, 04, 1), WeightKg = 40, WeightLbs = 88, UserId = 2 },
        new Weight { Date = new DateOnly(2025, 04, 2), WeightKg = 44, WeightLbs = 97, UserId = 2 },
    };
    
    
    public async void AddDailyWeightAsync(Weight weight)
    {
        List<Weight> usersWeights = await GetAllWeightsByUserIdAsync(weight.UserId);
        foreach (var w in usersWeights)
        {
            if(w.Date == weight.Date)
                throw new Exception("Daily weight already exists");
        }
        allweights.Add(weight);
    }

    public async Task<List<Weight>> GetAllWeightsByUserIdAsync(int userId)
    {
        return allweights.FindAll(x => x.UserId == userId);
    }

    public async Task<List<Weight>> GetAllAvgWeightsByUserIdAsync(int userId)
    {
        return allweights.FindAll(x => x.UserId == userId);
    }
}