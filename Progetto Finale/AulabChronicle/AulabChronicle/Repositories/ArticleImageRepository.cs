using AulabChronicle.Data; 
using AulabChronicle.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AulabChronicle.Repositories;

public class ArticleImageRepository : IArticleImageRepository
{
    private readonly AulabDbContext AulabDbContext;

    public ArticleImageRepository(AulabDbContext AulabDbContext)
    {
        this.AulabDbContext = AulabDbContext;
    }

    public async Task<Image> AddAsync(Image image)
    {
        await AulabDbContext.Images.AddAsync(image);
        await AulabDbContext.SaveChangesAsync();
        return image;
    }

    public async Task<Image?> DeleteByPathAsync(string path)
    {
        var existingImage = await AulabDbContext.Images.FirstOrDefaultAsync(x => x.Path == path);
        
        if (existingImage != null)
        {
            AulabDbContext.Images.Remove(existingImage);
            await AulabDbContext.SaveChangesAsync();
            return existingImage;
        }
        return null;
    }
}