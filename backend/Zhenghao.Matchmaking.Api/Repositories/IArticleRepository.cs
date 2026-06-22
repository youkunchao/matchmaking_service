using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IArticleRepository : IRepository<Article>
{
    Task<List<Article>> GetPublishedArticlesAsync(int page, int pageSize);
    Task<Article?> GetArticleByIdAsync(Guid id);
    Task<List<Article>> GetArticlesByCategoryAsync(string category);
}