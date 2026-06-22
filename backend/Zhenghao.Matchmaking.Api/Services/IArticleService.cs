using Zhenghao.Matchmaking.Api.Entities;

namespace Zhenghao.Matchmaking.Api.Services;

public interface IArticleService
{
    Task<List<Article>> GetPublishedArticlesAsync(int page, int pageSize);
    Task<Article?> GetArticleByIdAsync(Guid id);
    Task<Article> CreateArticleAsync(Article article);
    Task<Article> UpdateArticleAsync(Guid id, Article article);
    Task<bool> DeleteArticleAsync(Guid id);
    Task<Article> PublishArticleAsync(Guid id);
    Task IncrementViewsAsync(Guid id);
}