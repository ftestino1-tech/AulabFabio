using System.Runtime.CompilerServices;
using AulabChronicle.Data; 
using AulabChronicle.Models.Domain; 
using Microsoft.EntityFrameworkCore; 

namespace AulabChronicle.Repositories; 

public class CategoryRepository : ICategoryRepository
{
    private readonly AulabDbContext AulabDbContext; 

    public CategoryRepository(AulabDbContext aulabDbContext)
    {
        this.AulabDbContext = aulabDbContext; 
    }

    public async Task<IEnumerable<Category>> GetAllASync()
    {
        return await AulabDbContext.Categories
            .Include(c => c.Articles)
            .ToListAsync(); 
    }

    public async Task<Category?> GetAsync(long id)
    {
        return await AulabDbContext.Categories
            .Include(c => c.Articles)
            .FirstOrDefaultAsync(c => c.Id == id); 
    }

    public async Task<Category> AddAsync(Category category)
    {
        await AulabDbContext.Categories.AddAsync(category); 
        await AulabDbContext.SaveChangesAsync(); 
        return category; 
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        var existingCategory = await AulabDbContext.Categories.FindAsync(category.Id); 

        if (existingCategory != null)
        {
            existingCategory.Name = category.Name; 
            await AulabDbContext.SaveChangesAsync(); 
            return existingCategory; 
        }

        return null; 
    }

    public async Task<Category?> DeleteAsync(long id)
    {
        var existingCategory = await AulabDbContext.Categories.FindAsync(id); 

        if (existingCategory != null)
        {
            AulabDbContext.Categories.Remove(existingCategory); 
            await AulabDbContext.SaveChangesAsync(); 
            return existingCategory;
        }

        return null; 
    }

}