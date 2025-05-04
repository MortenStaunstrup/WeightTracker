using Core;

namespace WeightTrackerAPI.Repositories.Interfaces;

public interface IWeightRepository
{
    void AddDailyWeightAsync(Weight weight);
    Task<List<Weight>?> GetAllWeightsByUserIdAsync(int userId);
    Task<List<Weight>?> GetAllAvgWeightsByUserIdAsync(int userId);
    Task<Weight?> GetLatestTrackedWeightByUserIdAsync(int userId);
    void UpdateTodaysWeightAsync(Weight weight);
}