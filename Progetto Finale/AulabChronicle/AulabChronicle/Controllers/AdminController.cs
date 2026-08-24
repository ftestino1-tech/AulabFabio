using AulabChronicle.Models.Domain;
using AulabChronicle.Models.ViewModels;
using AulabChronicle.Repositories; 
using AulabChronicle.Services; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 

namespace AulabChronicle.Controllers; 

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ICareerRequestRepository careerRequestRepository; 
    private readonly ICrudService<CategoryDto, Category, long> categoryService; 

    public AdminController(
        ICareerRequestRepository careerRequestRepository, 
        ICrudService<CategoryDto, Category, long> categoryService)
    {
        this.careerRequestRepository = careerRequestRepository;
        this.categoryService = categoryService;        
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var requests = await careerRequestRepository.FindByIsCheckedAsync(false); 
        var categories = await categoryService.ReadAllAsync(); 

        ViewBag.Requests = requests; 
        ViewBag.Categories = categories; 

        return View(); 
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View(new Category());
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            await categoryService.CreateAsync(category, User, null); 

            TempData["SuccessMessage"] = "Categoria aggiunta con successo!";

            return RedirectToAction("Dashboard"); 
        }

        return View(category); 
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(long id)
    {
        var categories = await categoryService.ReadAllAsync(); 
        var categoryDto = categories.FirstOrDefault(c => c.Id == id); 

        if (categoryDto == null)
        {
            return NotFound(); 
        }

        var category = new Category
        {
            Id = categoryDto.Id, 
            Name = categoryDto.Name
        };

        return View(category); 
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            await categoryService.UpdateAsync(category.Id, category, null);

            TempData["SuccessMessage"] = "Categoria modificata con successo!"; 

            return RedirectToAction("Dashboard");
        }

        return View(category); 
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(long id)
    {
        await categoryService.DeleteAsync(id); 

        TempData["SuccessMessage"] = "Categoria cancellata con successo!";

        return RedirectToAction("Dashboard");
    }


}