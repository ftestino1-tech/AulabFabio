using System.Security.Claims;
using AulabChronicle.Models.Domain; 
using AulabChronicle.Models.ViewModels;
using AulabChronicle.Repositories; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AulabChronicle.Services; 

public class ArticleService : ICrudService<ArticleDto, Article, long>
{
    private readonly IArticleRepository articleRepository; 
    private readonly UserManager<IdentityUser> userManager;
    private readonly IImageService imageService;

    public UserManager<IdentityUser> UserManager => userManager; 

    public ArticleService(
        IArticleRepository articleRepository, 
        UserManager<IdentityUser> userManager, 
        IImageService imageService)
    {
        this.articleRepository = articleRepository; 
        this.userManager = userManager;
        this.imageService = imageService;
    }

    public async Task<ArticleDto> CreateAsync(Article article, ClaimsPrincipal principal, IFormFile? file)
    {
        var user = await userManager.GetUserAsync(principal); 

        if (user != null)
        {
            article.UserId = user.Id; 
        }

        string? imageUrl = null; 

        if (file != null && file.Length > 0)
        {
            imageUrl = await imageService.UploadAsync(file); 
        }

        article.IsAccepted = null; 
        
        var savedArticle = await articleRepository.AddAsync(article); 

        if (imageUrl != null)
        {
            await imageService.SaveToDbAsync(imageUrl, savedArticle.Id);
        }

        var finalArticle = await articleRepository.GetAsync(savedArticle.Id);

        return new ArticleDto
        {
            Id = finalArticle!.Id, 
            Title = finalArticle.Title, 
            Subtitle = finalArticle.Subtitle, 
            Body = finalArticle.Body, 
            PublishDate = finalArticle.PublishDate, 
            CreatedAt = finalArticle.CreatedAt, 
            User = finalArticle.User, 
            Category = finalArticle.Category, 
            Image = finalArticle.Image
        };
    }
    
    public async Task<List<ArticleDto>> ReadAllAsync()
    {
        var articles = await articleRepository.GetAllASync(); 

        return articles.Select(a => new ArticleDto
        {
            Id = a.Id,
            Title = a.Title, 
            Subtitle = a.Subtitle, 
            Body = a.Body, 
            PublishDate = a.PublishDate, 
            CreatedAt = a.CreatedAt, 
            IsAccepted = a.IsAccepted, 
            User = a.User, 
            Category = a.Category,
            Image = a.Image
        }).ToList();
    }

    public async Task<ArticleDto?> ReadAsync(long key)
    {
        var article = await articleRepository.GetAsync(key); 

        if (article == null)
        {
            return null; 
        }

        return new ArticleDto
        {
            Id = article.Id,
            Title = article.Title, 
            Subtitle = article.Subtitle, 
            Body = article.Body, 
            PublishDate = article.PublishDate, 
            CreatedAt = article.CreatedAt, 
            IsAccepted = article.IsAccepted, 
            User = article.User, 
            Category = article.Category, 
            Image = article.Image
        };
    }

    public async Task<ArticleDto?> UpdateAsync(long key, Article updatedArticle, IFormFile? file)
    {
        var existingArticle = await articleRepository.GetAsync(key);

        if (existingArticle == null)
        {
            return null; 
        }

        bool isModified = false; 

        if (existingArticle.Title != updatedArticle.Title ||
            existingArticle.Subtitle != updatedArticle.Subtitle ||
            existingArticle.Body != updatedArticle.Body ||
            existingArticle.CategoryId != updatedArticle.CategoryId)
        {
            isModified = true;
        }

        existingArticle.Title = updatedArticle.Title;
        existingArticle.Subtitle = updatedArticle.Subtitle;
        existingArticle.Body = updatedArticle.Body;
        existingArticle.CategoryId = updatedArticle.CategoryId;

        if (file != null && file.Length > 0)
        {
            if (existingArticle.Image != null)
            {
                await imageService.DeleteAsync(existingArticle.Image.Path);
            }

            var newImageUrl = await imageService.UploadAsync(file); 

            if (existingArticle.Image != null)
            {
                existingArticle.Image.Path = newImageUrl; 
            }
            else
            {
                existingArticle.Image = new Image
                {
                    Path = newImageUrl, 
                    ArticleId = key
                };
            }

            isModified = true; 
        }

        if (isModified)
        {
            existingArticle.IsAccepted = null; 
        }

        await articleRepository.UpdateAsync(existingArticle);

        return await ReadAsync(key);
    }
    
    public async Task<bool> DeleteAsync(long key)
    {
        var article = await articleRepository.DeleteAsync(key);

        if (article == null)
        {
            return false;
        }

        if (article.Image != null)
        {
            await imageService.DeleteAsync(article.Image.Path);
        }

        await articleRepository.DeleteAsync(key);

        return true;
    }

    public async Task<IEnumerable<ArticleDto>> SearchAsync(string searchTerm)
    {
        var articles = await articleRepository.SearchAsync(searchTerm);

        return articles
            .Where(a => a.IsAccepted == true)
            .OrderByDescending(a => a.PublishDate ?? a.CreatedAt)
            .Select(a => new ArticleDto
            {
                Id= a.Id,
                Title = a.Title,
                Subtitle= a.Subtitle,
                Body = a.Body,
                CreatedAt = a.CreatedAt,
                PublishDate = a.PublishDate,
                IsAccepted = a.IsAccepted,
                User = a.User, 
                Category = a.Category,
                Image = a.Image
            });
    }
}