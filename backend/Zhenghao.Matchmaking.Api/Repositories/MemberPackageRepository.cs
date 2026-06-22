using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class MemberPackageRepository : IMemberPackageRepository
{
    private readonly MatchmakingDbContext _context;
    private readonly DbSet<MemberPackage> _dbSet;

    public MemberPackageRepository(MatchmakingDbContext context)
    {
        _context = context;
        _dbSet = context.Set<MemberPackage>();
    }

    public async Task<List<MemberPackage>> GetActivePackagesAsync()
    {
        return await _dbSet
            .Where(p => p.Status == 1)
            .OrderBy(p => p.SortOrder)
            .ToListAsync();
    }

    public async Task<MemberPackage?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<MemberPackage?> GetPackageByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<MemberPackage> AddAsync(MemberPackage entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<MemberPackage> UpdateAsync(MemberPackage entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetPackageByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}