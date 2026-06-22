using Microsoft.EntityFrameworkCore;
using Zhenghao.Matchmaking.Api.Data;
using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public class ArticleRepository : GenericRepository<Article>, IArticleRepository
{
    public ArticleRepository(MatchmakingDbContext context) : base(context)
    {
    }

    public async Task<List<Article>> GetPublishedArticlesAsync(int page, int pageSize)
    {
        return await _context.Articles
            .Where(a => a.Status == 1)
            .OrderByDescending(a => a.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Article?> GetArticleByIdAsync(Guid id)
    {
        return await _context.Articles
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Article>> GetArticlesByCategoryAsync(string category)
    {
        return await _context.Articles
            .Where(a => a.Status == 1 && a.Category == category)
            .OrderByDescending(a => a.PublishedAt)
            .ToListAsync();
    }
}