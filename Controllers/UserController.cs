using System.Security.Claims;
using AlbBlogger1.Areas;
using AlbBlogger1.Data;
using AlbBlogger1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Services.Extensions;

namespace AlbBlogger1.Controllers;

[Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IRoleService roleService, IUserRoleService userRoleService,IConfiguration configuration, IFileHandlerService fileHandlerService,IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _roleService = roleService;
            _userRoleService = userRoleService;
            _fileHandlerService = fileHandlerService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;


        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.FindAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userRepository.GetUserByIdAsync(id);
            await _userRepository.DeleteUserAsync(user);

            var userRole = await _userRoleService.GetUserRoleByUserIdAsync(user.Id);
            await _userRoleService.DeleteAsync(userRole);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            await _userRepository.UpdateUserAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }
     
        // ketu do te krijojme metodat per applicationuserrole.

        [HttpGet]
        public async Task<IActionResult> UserRoleIndex(string userId)
        {
            var userRoles = (await _userRoleService.GetAllAsync()).Where(x => x.UserId == userId);
            var user = await _userRepository.GetUserByIdAsync(userId);
            // get userrole
            var userRole = await _userRoleService.GetUserRoleByUserIdAsync(user.Id);
            var role = await _roleService.GetRoleByUserIdAsync(userRole.RoleId);
            ViewBag.RoleName  = role.Name;
            ViewBag.UserName = user.CustomUserName;
            ViewBag.UserId = userId;
            return View(userRoles);
        }

        [HttpGet]
        public async Task<IActionResult> UserRoleDelete(string id)
        {
            var user = await _userRoleService.GetUserRoleByRoleIdAsync(id);
            await _userRoleService.DeleteAsync(user);
            return RedirectToAction("UserRoleIndex");
        }

        [HttpGet]
        public async Task<IActionResult> UserRoleCreate(string userId)
        {
            ViewBag.UserId = userId;

            ApplicationUserRole userRole = new ApplicationUserRole();

            userRole.UserId = userId;
            var roles = await _roleService.GetAllAsync();
            ViewBag.Roles = roles;
            return View(userRole);

        }
        [HttpPost]
        public async Task<IActionResult> UserRoleCreate(ApplicationUserRole userRole)
        {
            await _userRoleService.CreateAsync(userRole);
            return RedirectToAction("UserRoleIndex", new { userId = userRole.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> UserRoleEdit(string id)
        {
            var userRole = await _userRoleService.GetUserRoleByRoleIdAsync(id);

            return View(userRole);
        }
        [HttpPost]
        public async Task<IActionResult> UserRoleEdit(ApplicationUserRole userRole)
        {
            await _userRoleService.UpdateAsync(userRole);
            return RedirectToAction("UserRoleIndex");
        }

        [HttpGet]
        public async Task<IActionResult> UserRoleDetails(string id)
        {
            var userRole = await _userRoleService.GetUserRoleByRoleIdAsync(id);
            return View(userRole);
        }
        
        
        // ----------------------------- profile picture ----------------------------------
        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile profilePicture)
        {
            if (profilePicture == null || profilePicture.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var fileName = await _fileHandlerService.UploadProfilePictureAsync(profilePicture, userId);
        
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }

                // Remove old profile picture if it exists
                if (!string.IsNullOrEmpty(user.ProfilePicture))
                {
                    _fileHandlerService.RemoveImageFile(_configuration["ProfilePictures:ProfilePictures"], user.ProfilePicture);
                }

                user.ProfilePicture = fileName;
                await _userRepository.UpdateUserAsync(user);

                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("EditProfile");
            }
        }
    }