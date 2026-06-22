using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IActivityRepository : IRepository<Activity>
{
    Task<List<Activity>> GetByStatusAsync(int status);
    Task<List<Activity>> GetUpcomingActivitiesAsync();
    Task<bool> HasParticipatedAsync(Guid activityId, Guid userId);
}