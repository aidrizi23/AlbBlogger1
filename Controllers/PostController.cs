using AlbBlogger1.Data;
using AlbBlogger1.Models;
using AlbBlogger1.Repositories.Pagination;
using AlbBlogger1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlbBlogger1.Controllers;

public class PostController : Controller
{
    private readonly IPostService _postService;
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;

    public PostController(IPostService postService, IUserService userService, UserManager<ApplicationUser> userManager)
    {
        _postService = postService;
        _userService = userService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int pageIndex = 1)
    {
        const int pageSize = 10;
        ViewBag.PageIndex = pageIndex;
        var posts = await _postService.GetAllPaginatedPostsAsync();
        return View(posts);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    // GET: Post/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    // POST: Post/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        await _postService.DeletePostAsync(post);
        return RedirectToAction("Index");
    }
    
    
    
    
    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new PostViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PostViewModel viewModel)
    {
       
            var user = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(User);
            var post = new Post()
            {
                Title = viewModel.Title,
                Content = viewModel.Content,
                PublishDate = viewModel.PublishDate,
                Tags = viewModel.Tags,
                Likes = viewModel.Likes, // Set default values or handle them as needed
                Views = 0,
                Image = viewModel.Image,
                UserId = userId,
                User = user,
            };

            await _postService.CreatePostAsync(post); // Assuming _postService is injected

            return RedirectToAction("Index");
       
    }
    
    
    // --------------------------- FIlters ------------------------
    public async Task<IActionResult> FilterByHighestLikes(int pageIndex = 1, int pageSize = 10)
    {
        var posts = await _postService.GetPaginatedPostsByHighestLikes(pageIndex, pageSize);

        // Firstly, let's get the posts of the logged-in user
        var user = await _userManager.GetUserAsync(User);
        var userId = _userManager.GetUserId(User);

        if (user != null)
            // Filter posts by the logged-in user's ID
            posts = new PaginatedList<Post>(
                posts.Where(x => x.UserId == userId).ToList(),
                posts.TotalCount,
                pageIndex,
                pageSize
            );

        return View("Index", posts);
    }

}