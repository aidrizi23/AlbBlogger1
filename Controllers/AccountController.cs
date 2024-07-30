using System.ComponentModel.DataAnnotations;
using AlbBlogger1.Controllers;
using AlbBlogger1.Data;
using AlbBlogger1.Models;
using AlbBlogger1.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;


[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserRoleService _userRoleService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly ILikeService _likeService;

        public AccountController(
            ILikeService likeService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IUserRoleService userRoleService,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userRoleService = userRoleService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _likeService = likeService;
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
                    Id = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CustomUserName = $"{model.FirstName}_{model.LastName}"
                    
                };

                if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
                {
                    var fileName = await UploadProfilePicture(model.ProfilePicture, user.Id);
                    user.ProfilePicture = fileName;
                }
            
                var result = await _userManager.CreateAsync(user, model.Password);
        
                var userRole = new ApplicationUserRole()
                {
                    RoleId = "a14bs9c0-aa65-4af8-bd17-00bd9344e575",
                    UserId = user.Id
                };
                await _userRoleService.CreateAsync(userRole);
        
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        
        private async Task<string> UploadProfilePicture(IFormFile file, string userId)
        {
            var uploadDir = _configuration["ProfilePictures:ProfilePictures"];
            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir);

            Directory.CreateDirectory(uploads);

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Invalid file type. Only jpg, jpeg, png, and gif are allowed.");
            }

            var fileName = $"{userId}{fileExtension}";
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        
        // method to get the liked posts of an user
        public async Task<IActionResult> LikedPostsByUser()
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;
            var posts = await _likeService.GetLikedPostsByUserId(userId);
            return View(posts);

        }




    }
