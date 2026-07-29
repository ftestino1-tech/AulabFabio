using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager; 


        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(); 
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            var result = signInManager.PasswordSignInAsync(
                                model.Username, 
                                model.Password, 
                                false, 
                                false)
                            .GetAwaiter()
                            .GetResult(); 

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home"); 
            }            

            ModelState.AddModelError("", "Username o password non validi"); 
            return View(model); 
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var createResult = userManager.CreateAsync(user, model.Password)
                                          .GetAwaiter()
                                          .GetResult(); 

            if (createResult.Succeeded)
            {
                var roleResult = userManager.AddToRoleAsync(user, "User")
                                            .GetAwaiter()
                                            .GetResult(); 

                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Register"); 
                }   

                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description); 
                }

                return View(model); 
            }

            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description); 
            }

            return View(model); 
        }

        [HttpGet]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync()
                        .GetAwaiter()
                        .GetResult();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}