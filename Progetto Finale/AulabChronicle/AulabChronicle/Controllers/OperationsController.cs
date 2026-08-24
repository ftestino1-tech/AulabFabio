using AulabChronicle.Models.Domain; 
using AulabChronicle.Services; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc; 

namespace AulabChronicle.Controllers; 

[Authorize]
public class OperationsController : Controller
{
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<IdentityUser> userManager; 
    private readonly ICareerRequestService careerRequestService; 

    public OperationsController(
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager,
        ICareerRequestService careerRequestService)
    {
        this.roleManager = roleManager;
        this.userManager = userManager; 
        this.careerRequestService = careerRequestService; 
    }

    [HttpGet]
    public async Task<IActionResult> CareerRequest()
    {
        var userId = userManager.GetUserId(User); 
        if (userId == null) return Unauthorized(); 

        var allRoles = roleManager.Roles
            .Where(r => r.Name != "Admin" && r.Name != "User")
            .ToList(); 

        var availableRoles = new List<IdentityRole>();

        foreach (var role in allRoles)
        {
            var isAssigned = await careerRequestService.IsRoleAlreadyAssignedAsync(userId, role.Id); 
            var isPending = await careerRequestService.IsRequestPendingAsync(userId, role.Id); 

            if (!isAssigned && !isPending)
            {
                availableRoles.Add(role); 
            }
        }

        ViewBag.Roles = availableRoles;

        if (!availableRoles.Any())
        {
            TempData["ErrorMessage"] = "Hai già richiesto o possiedi tutti i ruoli disponibili"; 
            return RedirectToAction("Index", "Home");
        }

        return View(new CareerRequest());
    }

    [HttpPost]
    public async Task<IActionResult> CareerRequestStore(CareerRequest careerRequest)
    {
        var userId = userManager.GetUserId(User); 
        if (userId == null) return Unauthorized(); 

        if (string.IsNullOrEmpty(careerRequest.RoleId))
        {
            ModelState.AddModelError("RoleId", "Seleziona un ruolo valido."); 
        }

        if (!ModelState.IsValid)
        {
            var allRoles = roleManager.Roles
                .Where(r => r.Name != "Admin" && r.Name != "User")
                .ToList(); 

            var availableRoles = new List<IdentityRole>(); 

            foreach (var role in allRoles)
            {
                if (!await careerRequestService.IsRoleAlreadyAssignedAsync(userId, role.Id) &&
                    !await careerRequestService.IsRequestPendingAsync(userId, role.Id))
                {
                    availableRoles.Add(role);     
                }
            }

            ViewBag.Roles = availableRoles; 
            return View("CareerRequest", careerRequest); 
        }

       if (await careerRequestService.IsRoleAlreadyAssignedAsync(userId, careerRequest.RoleId))
        {
            TempData["ErrorMessage"] = "Possiedi già questo ruolo"; 
            return RedirectToAction("Index", "Home"); 
        }

        if (await careerRequestService.IsRequestPendingAsync(userId, careerRequest.RoleId))  
        {
            TempData["ErrorMessage"] = "Hai già una richiesta in sospeso per questo ruolo"; 
            return RedirectToAction("Index", "Home"); 
        }

        careerRequest.UserId = userId; 
        await careerRequestService.SaveAsync(careerRequest); 

        TempData["SuccessMessage"] = "Richiesta inviata con successo"; 
        return RedirectToAction("Index", "Home"); 
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> CareerDetail(long id)
    {
        var request = await careerRequestService.FindAsync(id); 

        if (request == null)
        {
            return NotFound(); 
        }

        return View(request);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CareerAccept(long id)
    {
        await careerRequestService.AcceptAsync(id); 

        TempData["SuccessMessage"] = "Ruolo abilitato per l'utente";

        return RedirectToAction("Dashboard", "Admin");
    }
}