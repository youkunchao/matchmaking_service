using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class HongniangRepository : GenericRepository<Hongniang>, IHongniangRepository
{
    private readonly MatchmakingDbContext _context;

    public HongniangRepository(MatchmakingDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Hongniang?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(h => h.Username == username);
    }

    public async Task<List<Hongniang>> GetActiveHongniangAsync()
    {
        return await _dbSet.Where(h => h.Status == 1).ToListAsync();
    }

    public async Task<List<User>> GetUsersByHongniangIdAsync(Guid hongniangId)
    {
        return await _context.Users.Where(u => u.HongniangId == hongniangId).ToListAsync();
    }
}