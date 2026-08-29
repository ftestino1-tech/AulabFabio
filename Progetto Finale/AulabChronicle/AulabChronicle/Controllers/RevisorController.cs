using AulabChronicle.Models.Domain;
using AulabChronicle.Models.ViewModels; 
using AulabChronicle.Repositories; 
using AulabChronicle.Services; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 

namespace AulabChronicle.Controllers; 

[Authorize(Roles = "Revisor")]
public class RevisorController : Controller
{
    private readonly IArticleRepository articleRepository;
    private readonly ArticleService articleService; 

    public RevisorController(IArticleRepository articleRepository, ArticleService articleService)
    {
        this.articleRepository = articleRepository; 
        this.articleService = articleService;
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var articles = (await articleRepository.GetAllAsync())
            .Where(a => a.IsAccepted == null)
            .OrderByDescending(a => a.CreatedAt)
            .ToList();

        var articlesDtos = articles.Select(a => new ArticleDto
        {
            Id = a.Id, 
            Title = a.Title, 
            Subtitle = a.Subtitle, 
            Body = a.Body, 
            CreatedAt = a.CreatedAt, 
            PublishDate = a.PublishDate, 
            IsAccepted = a.IsAccepted, 
            User = a.User, 
            Category = a.Category, 
            Image = a.Image
        }).ToList();

        return View(articlesDtos); 
    }

    [HttpGet]
    public async Task<IActionResult> Detail(long id)
    {
        var articles = await articleRepository.GetAllAsync(); 
        var article = articles.FirstOrDefault(a => a.Id == id); 

        if (article == null)
        {
            return NotFound();
        }

        var dto = new ArticleDto
        {
            Id = article.Id, 
            Title = article.Title, 
            Subtitle = article.Subtitle, 
            Body = article.Body, 
            CreatedAt = article.CreatedAt, 
            PublishDate = article.PublishDate, 
            IsAccepted = article.IsAccepted, 
            User = article.User, 
            Category = article.Category,
            Image = article.Image
        };

        return View(dto); 
    }

    [HttpPost]
    public async Task<IActionResult> SetAccepted(long id, bool accepted)
    {
        var articles = await articleRepository.GetAllAsync();
        var article = articles.FirstOrDefault(a => a.Id == id); 

        if (article != null)
        {
            article.IsAccepted = accepted; 

            if (accepted)
            {
                article.PublishDate = DateTime.Now; 
            }

            await articleRepository.UpdateAsync(article); 

            TempData["SuccessMessage"] = accepted
                ? "Articolo accettato!"
                : "Articolo rifiutato!"; 
        }

        return RedirectToAction("Dashboard"); 
    }
}