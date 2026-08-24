using AulabChronicle.Models.ViewModels;
using AulabChronicle.Repositories; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc; 

namespace AulabChronicle.Controllers;

[Authorize(Roles = "Writer")]
public class WriterController : Controller
{
    private readonly IArticleRepository articleRepository;
    private readonly UserManager<IdentityUser> userManager;

    public WriterController(IArticleRepository articleRepository, UserManager<IdentityUser> userManager)
    {
        this.articleRepository = articleRepository; 
        this.userManager = userManager; 
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var user = await userManager.GetUserAsync(User); 

        if (user == null)
        {
            return Unauthorized(); 
        }

        var articles = (await articleRepository.GetAllASync())
            .Where(a => a.UserId == user.Id)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new ArticleDto
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
            })
            .ToList(); 
        
        return View(articles);
    }
}