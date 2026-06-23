using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Repositories;

public interface IArticleRepository
{
    Task<List<Article>> GetPublishedArticlesAsync(int page, int pageSize);
    Task<Article?> GetArticleByIdAsync(Guid id);
    Task<List<Article>> GetArticlesByCategoryAsync(string category);
    Task<Article> AddAsync(Article entity);
    Task<Article> UpdateAsync(Article entity);
    Task DeleteAsync(Guid id);
}