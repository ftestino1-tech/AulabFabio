using Microsoft.AspNetCore.Mvc; 
using AulabChronicle.Models.Domain;
using AulabChronicle.Services; 
using Microsoft.AspNetCore.Authorization;

namespace AulabChronicle.Controllers; 

public class ArticleController : Controller
{
    private readonly CategoryService categoryService;
    private readonly ArticleService articleService; 

    public ArticleController(CategoryService categoryService, ArticleService articleService)
    {
        this.categoryService = categoryService; 
        this.articleService = articleService; 
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var articles = (await articleService.ReadAllAsync())
            .Where(a => a.IsAccepted == true)
            .ToList();

        var orderedArticles = articles
            .OrderByDescending(a => a.PublishDate ?? a.CreatedAt)
            .ToList();

        ViewBag.Title = "Tutti gli articoli"; 

        return View(orderedArticles); 
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await categoryService.ReadAllAsync(); 

        ViewBag.Categories = categories; 

        return View(new Article()); 
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(Article article, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            await articleService.CreateAsync(article, User, file); 

            TempData["SuccessMessage"] = "Articolo aggiunto con successo ed inviato in revisione!"; 

            return RedirectToAction("Index", "Home"); 
        }
        ViewBag.Categories = await categoryService.ReadAllAsync();
        
        return View(article); 
    }
    
    [HttpGet]
    public async Task<IActionResult> Search(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return RedirectToAction("Index");
        }

        var articles = await articleService.SearchAsync(keyword);

        ViewBag.Title = $"Risultati della ricerca per: {keyword}";
        return View("Index", articles);
    }

    [HttpGet]
    public async Task<IActionResult> Details(long id)
    {
        var article = (await articleService.ReadAllAsync()).FirstOrDefault(a => a.Id == id); 

        if (article == null)
        {
            return NotFound(); 
        }

        return View(article); 
    }

    [Authorize(Roles = "Writer")]
    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var articleDto = await articleService.ReadAsync(id); 

        if (articleDto == null)
        {
            return NotFound();
        }

        var user = await articleService.UserManager.GetUserAsync(User); 

        if (articleDto.User?.Id != user?.Id)
        {
            return Forbid(); 
        }        

        var article = new Article
        {
            Id = articleDto.Id, 
            Title = articleDto.Title, 
            Subtitle = articleDto.Subtitle, 
            Body = articleDto.Body, 
            CategoryId = articleDto.Category?.Id ?? 0, 
            Image = articleDto.Image
        };

        ViewBag.Categories = await categoryService.ReadAllAsync();

        return View(article); 
    }
    

    [Authorize(Roles="Writer")]
    [HttpPost]
    public async Task<IActionResult> Update(long id, Article article, IFormFile? file)
    {
        var existingArticle = await articleService.ReadAsync(id); 

        if (existingArticle == null)
        {
            return NotFound(); 
        }

        var user = await articleService.UserManager.GetUserAsync(User); 

        if (existingArticle.User?.Id != user?.Id)
        {
            return Forbid(); 
        }

        if (ModelState.IsValid)
        {
            await articleService.UpdateAsync(id, article, file); 

            TempData["SuccessMessage"] = "Articolo modificato correttamente ed inviato in revisione!"; 

            return RedirectToAction("Dashboard", "Writer"); 
        }

        ViewBag.Categories = await categoryService.ReadAllAsync(); 

        return View("Edit", article); 
    }

    [Authorize(Roles = "Writer")]
    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        var existingArticle = await articleService.ReadAsync(id); 

        if (existingArticle == null)
        {
            return NotFound(); 
        }

        var user = await articleService.UserManager.GetUserAsync(User); 

        if (existingArticle.User?.Id != user?.Id)
        {
            return Forbid(); 
        }

        await articleService.DeleteAsync(id); 

        TempData["SuccessMessage"] = "Articolo eliminato con successo!";

        return RedirectToAction("Dashboard", "Writer"); 
    }

}

