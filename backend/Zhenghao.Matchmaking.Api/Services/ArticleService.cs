using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Repositories;

namespace Zhenghao.Matchmaking.Api.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<List<Article>> GetPublishedArticlesAsync(int page, int pageSize)
    {
        return await _articleRepository.GetPublishedArticlesAsync(page, pageSize);
    }

    public async Task<Article?> GetArticleByIdAsync(Guid id)
    {
        return await _articleRepository.GetArticleByIdAsync(id);
    }

    public async Task<Article> CreateArticleAsync(Article article)
    {
        article.CreatedAt = DateTime.Now;
        article.UpdatedAt = DateTime.Now;
        return await _articleRepository.AddAsync(article);
    }

    public async Task<Article> UpdateArticleAsync(Guid id, Article article)
    {
        var existingArticle = await _articleRepository.GetArticleByIdAsync(id);
        if (existingArticle == null)
            throw new Exception("文章不存在");

        existingArticle.Title = article.Title;
        existingArticle.Content = article.Content;
        existingArticle.Summary = article.Summary;
        existingArticle.CoverImage = article.CoverImage;
        existingArticle.Category = article.Category;
        existingArticle.Author = article.Author;
        existingArticle.UpdatedAt = DateTime.Now;

        return await _articleRepository.UpdateAsync(existingArticle);
    }

    public async Task<bool> DeleteArticleAsync(Guid id)
    {
        var article = await _articleRepository.GetArticleByIdAsync(id);
        if (article == null)
            return false;

        await _articleRepository.DeleteAsync(id);
        return true;
    }

    public async Task<Article> PublishArticleAsync(Guid id)
    {
        var article = await _articleRepository.GetArticleByIdAsync(id);
        if (article == null)
            throw new Exception("文章不存在");

        article.Status = 1;
        article.PublishedAt = DateTime.Now;
        article.UpdatedAt = DateTime.Now;

        return await _articleRepository.UpdateAsync(article);
    }

    public async Task IncrementViewsAsync(Guid id)
    {
        var article = await _articleRepository.GetArticleByIdAsync(id);
        if (article != null)
        {
            article.Views++;
            await _articleRepository.UpdateAsync(article);
        }
    }
}