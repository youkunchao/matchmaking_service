using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class MemberPackageRepository : GenericRepository<MemberPackage>, IMemberPackageRepository
{
    public MemberPackageRepository(MatchmakingDbContext context) : base(context)
    {
    }

    public async Task<List<MemberPackage>> GetActivePackagesAsync()
    {
        return await _dbSet.Where(p => p.Status == 1).ToListAsync();
    }
}