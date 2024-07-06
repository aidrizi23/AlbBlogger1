using System.ComponentModel.DataAnnotations;
using AlbBlogger1.Controllers;
using AlbBlogger1.Data;
using AlbBlogger1.Models;
using AlbBlogger1.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;


[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserRoleService _userRoleService;

        public AccountController(UserManager<ApplicationUser> userManager, 
                                SignInManager<ApplicationUser> signInManager, 
                                ILogger<AccountController> logger,
                                IUserRoleService userRoleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userRoleService = userRoleService;
        }

        [HttpGet]
        [AllowAnonymous] //  lejon userin te navigoje ne webpage edhe pse nuk eshte i loguar sepse useri sdo jete i loguar deri ketu
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); // ben clear te gjitha cookies (funksionon edhe pa kete rresht kodi) per te bere nje clear login process
            ViewBag.Error = false;
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If model state is not valid, return to login view with errors
            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"Logout");
            return RedirectToAction("Login", "Account");

        }


        [HttpGet]
        [AllowAnonymous]    
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Id = Guid.NewGuid().ToString(), // Ensure Id is set
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                };
                    
                var result = await _userManager.CreateAsync(user, model.Password);
                
                
                // TEK KJO METODE, DO TE BEJME QE TE KENE TE GJITHE KETA ROLIN NORMAL USER
                var userRole = new ApplicationUserRole()
                {
                    RoleId = "a14bs9c0-aa65-4af8-bd17-00bd9344e575",
                    UserId = user.Id
                };
                await _userRoleService.CreateAsync(userRole); // krijojme automatikisht user role
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");

                   return RedirectToAction("Index", "Home"); // Redirect to returnUrl or default homepage
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If model state is not valid or registration fails, return to register view with errors
            return View(model);
        }




    }
