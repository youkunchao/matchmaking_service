using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
{
    private readonly MatchmakingDbContext _context;

    public ActivityRepository(MatchmakingDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Activity>> GetByStatusAsync(int status)
    {
        return await _dbSet.Where(a => a.Status == status).ToListAsync();
    }

    public async Task<List<Activity>> GetUpcomingActivitiesAsync()
    {
        return await _dbSet
            .Where(a => a.Status == 0 || a.Status == 1)
            .OrderBy(a => a.StartTime)
            .ToListAsync();
    }

    public async Task<bool> HasParticipatedAsync(Guid activityId, Guid userId)
    {
        return await _context.ActivityParticipants
            .AnyAsync(ap => ap.ActivityId == activityId && ap.UserId == userId);
    }
}