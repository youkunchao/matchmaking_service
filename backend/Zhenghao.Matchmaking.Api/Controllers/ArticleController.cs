using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zhenghao.Matchmaking.Api.Entities;
using Zhenghao.Matchmaking.Api.Services;

namespace Zhenghao.Matchmaking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Article>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var articles = await _articleService.GetPublishedArticlesAsync(page, pageSize);
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetById(Guid id)
    {
        await _articleService.IncrementViewsAsync(id);
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
            return NotFound();
        return Ok(article);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Article>> Create([FromBody] Article article)
    {
        try
        {
            var result = await _articleService.CreateArticleAsync(article);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Article>> Update(Guid id, [FromBody] Article article)
    {
        try
        {
            var result = await _articleService.UpdateArticleAsync(id, article);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _articleService.DeleteArticleAsync(id);
        if (!result)
            return NotFound();
        return Ok();
    }

    [HttpPost("{id}/publish")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Article>> Publish(Guid id)
    {
        try
        {
            var result = await _articleService.PublishArticleAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}