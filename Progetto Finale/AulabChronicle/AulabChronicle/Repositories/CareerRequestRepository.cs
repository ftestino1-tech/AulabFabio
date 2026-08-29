using System.IO.Compression;
using AulabChronicle.Data; 
using AulabChronicle.Models.Domain; 
using Microsoft.EntityFrameworkCore; 

namespace AulabChronicle.Repositories; 

public class CareerRequestRepository : ICareerRequestRepository
{
    private readonly AulabDbContext AulabDbContext;

    public CareerRequestRepository(AulabDbContext AulabDbContext)
    {
        this.AulabDbContext = AulabDbContext; 
    }

    public async Task<CareerRequest> AddAsync(CareerRequest careerRequest)
    {
        await AulabDbContext.CareerRequests.AddAsync(careerRequest); 
        await AulabDbContext.SaveChangesAsync(); 
        return careerRequest; 
    }

    public async Task<IEnumerable<CareerRequest>> GetAllAsync()
    {
        return await AulabDbContext.CareerRequests
            .Include(x => x.User)
            .Include(x => x.Role)
            .ToListAsync();
    }

    public async Task<IEnumerable<CareerRequest>> FindByIsCheckedAsync(bool isChecked)
    {
        return await AulabDbContext.CareerRequests
            .Where(x => x.IsChecked == isChecked)
            .Include(x => x.User)
            .Include(x => x.Role)
            .ToListAsync();
    }

    public async Task<CareerRequest?> GetAsync(long id)
    {
        return await AulabDbContext.CareerRequests
            .Include(x => x.User)
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
    }   

    public async Task<CareerRequest?> UpdateAsync(CareerRequest careerRequest)
    {
        var existingRequest = await AulabDbContext.CareerRequests.FindAsync(careerRequest.Id); 

        if (existingRequest != null)
        {
            existingRequest.IsChecked = careerRequest.IsChecked; 
            await AulabDbContext.SaveChangesAsync(); 
            return existingRequest; 
        }

        return null; 
    }   

    public async Task<IEnumerable<CareerRequest>> GetByUserIdAsync(string userId)
    {
        return await AulabDbContext.CareerRequests
            .Where(x => x.UserId == userId)
            .Include(x => x.Role)
            .ToListAsync(); 
    }

}