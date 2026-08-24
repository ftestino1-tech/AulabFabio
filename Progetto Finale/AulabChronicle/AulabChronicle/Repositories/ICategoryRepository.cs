using AulabChronicle.Models.Domain; 

namespace AulabChronicle.Repositories; 

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllASync(); 
    Task<Category?> GetAsync(long id); 
    Task<Category> AddAsync(Category category); 
    Task<Category?> UpdateAsync(Category category); 
    Task<Category?> DeleteAsync(long id); 
}