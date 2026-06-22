using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class MeetRequestRepository : GenericRepository<MeetRequest>, IMeetRequestRepository
{
    public MeetRequestRepository(MatchmakingDbContext context) : base(context)
    {
    }

    public async Task<List<MeetRequest>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(m => m.UserId == userId).ToListAsync();
    }

    public async Task<List<MeetRequest>> GetByTargetUserIdAsync(Guid targetUserId)
    {
        return await _dbSet.Where(m => m.TargetUserId == targetUserId).ToListAsync();
    }

    public async Task<List<MeetRequest>> GetByStatusAsync(int status)
    {
        return await _dbSet.Where(m => m.Status == status).ToListAsync();
    }

    public async Task<MeetRequest?> GetBetweenUsersAsync(Guid userId, Guid targetUserId)
    {
        return await _dbSet.FirstOrDefaultAsync(m => 
            (m.UserId == userId && m.TargetUserId == targetUserId) ||
            (m.UserId == targetUserId && m.TargetUserId == userId));
    }
}