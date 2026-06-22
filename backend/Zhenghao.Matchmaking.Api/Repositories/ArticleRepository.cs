using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly MatchmakingDbContext _context;
    private readonly DbSet<Article> _dbSet;

    public ArticleRepository(MatchmakingDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Article>();
    }

    public async Task<List<Article>> GetPublishedArticlesAsync(int page, int pageSize)
    {
        return await _dbSet
            .Where(a => a.Status == 1)
            .OrderByDescending(a => a.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Article?> GetArticleByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Article>> GetArticlesByCategoryAsync(string category)
    {
        return await _dbSet
            .Where(a => a.Status == 1 && a.Category == category)
            .OrderByDescending(a => a.PublishedAt)
            .ToListAsync();
    }

    public async Task<Article> AddAsync(Article entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Article> UpdateAsync(Article entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetArticleByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}