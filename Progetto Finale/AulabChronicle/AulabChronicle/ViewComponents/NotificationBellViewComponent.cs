using AulabChronicle.Models.Domain;
using AulabChronicle.Repositories;
using AulabChronicle.Services;
using Microsoft.AspNetCore.Mvc; 

namespace AulabChronicle.ViewComponents; 

public class NotificationBellViewComponent : ViewComponent
{
    private readonly ICareerRequestRepository careerRequestRepository;
    private readonly IArticleRepository articleRepository; 

    public NotificationBellViewComponent(
        ICareerRequestRepository careerRequestRepository, 
        IArticleRepository articleRepository) 
        {
            this.careerRequestRepository = careerRequestRepository; 
            this.articleRepository = articleRepository; 
        }
        
        public async Task<IViewComponentResult> InvokeAsync(string role)
    {
        int count = 0; 

        if (role == "Admin")
        {
            count = (await careerRequestRepository.FindByIsCheckedAsync(false)).Count();
        }
        else if (role == "Revisor")
        {
            var articles = await articleRepository.GetAllASync(); 
            count = articles.Count(a => a.IsAccepted == null);
        }

        return View(count);
    }
}