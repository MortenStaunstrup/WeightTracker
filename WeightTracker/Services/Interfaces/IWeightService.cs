using Core;

namespace WeightTracker.Services.Interfaces;

public interface IWeightService
{
    void AddDailyWeightAsync(Weight weight);
    Task<List<Weight>?> GetAllWeightsByUserIdAsync(int userId);
    Task<List<Weight>?> GetAllAvgWeightsByUserIdAsync(int userId);
    Task<Weight?> GetLatestTrackedWeightByUserIdAsync(int userId);
    void UpdateWeight(Weight weight);
}