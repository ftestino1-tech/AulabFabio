using AulabChronicle.Models.Domain; 

namespace AulabChronicle.Services; 

public interface ICareerRequestService
{
    Task<bool> IsRoleAlreadyAssignedAsync(string userId, string roleId); 
    Task<bool> IsRequestPendingAsync(string userId, string roleId); 
    Task SaveAsync(CareerRequest careerRequest); 
    Task AcceptAsync(long requestId); 
    Task<CareerRequest?> FindAsync(long id); 
}






