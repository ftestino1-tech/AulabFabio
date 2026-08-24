using System.ComponentModel.DataAnnotations;
using AulabChronicle.Models.Domain;
using AulabChronicle.Repositories; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AulabChronicle.Services; 

public class CareerRequestService : ICareerRequestService
{
    private readonly ICareerRequestRepository careerRequestRepository; 
    private readonly UserManager<IdentityUser> userManager; 
    private readonly RoleManager<IdentityRole> roleManager; 
    private readonly IEmailService emailService; 

    public CareerRequestService(
        ICareerRequestRepository careerRequestRepository, 
        UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IEmailService emailService)
    {
        this.careerRequestRepository = careerRequestRepository; 
        this.userManager = userManager;
        this.roleManager = roleManager; 
        this.emailService = emailService; 
    }

    public async Task<bool> IsRoleAlreadyAssignedAsync(string userId, string roleId)
    {
        var user = await userManager.FindByIdAsync(userId); 
        if (user == null) return false; 

        var role = await roleManager.FindByIdAsync(roleId); 
        if (role == null) return false; 

        return await userManager.IsInRoleAsync(user, role.Name!);
    }

    public async Task<bool> IsRequestPendingAsync(string userId, string roleId)
    {
        var requests = await careerRequestRepository.GetByUserIdAsync(userId); 
        return requests.Any(r => r.RoleId == roleId && !r.IsChecked); 
    }

    public async Task SaveAsync(CareerRequest careerRequest)
    {
        careerRequest.IsChecked = false; 
        await careerRequestRepository.AddAsync(careerRequest);

        var user = await userManager.FindByIdAsync(careerRequest.UserId); 
        var role = await roleManager.FindByIdAsync(careerRequest.RoleId); 

        await emailService.SendEmailAsync(
            "adminAulabpost@admin.com", 
            $"Richiesta per ruolo: {role?.Name}", 
            $"C'è una nuova richiesta di collaborazione da parte di {user?.UserName}");
    }

    public async Task AcceptAsync(long requestId)
    {
        var request = await careerRequestRepository.GetAsync(requestId); 
        if (request == null) return; 

        var user = await userManager.FindByIdAsync(request.UserId); 
        var role = await roleManager.FindByIdAsync(request.RoleId);

        if (user != null && role != null)
        {
            await userManager.AddToRoleAsync(user, role.Name!); 

            request.IsChecked = true; 
            await careerRequestRepository.UpdateAsync(request); 

            await emailService.SendEmailAsync(
                user.Email!, 
                "Ruolo abilitato", 
                "Ciao, la tua richiesta di collaborazione è stata accettata dalla nostra amministrazione");
        }
    }

    public async Task<CareerRequest?> FindAsync(long id)
    {
        return await careerRequestRepository.GetAsync(id); 
    }

}