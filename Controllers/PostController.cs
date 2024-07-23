using System.Security.AccessControl;
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
    
    private readonly IBookmarkService _bookmarkService;

    public PostController(IPostService postService, IUserService userService, UserManager<ApplicationUser> userManager, IBookmarkService bookmarkService)
    {
        _postService = postService;
        _userService = userService;
        _userManager = userManager;
        _bookmarkService = bookmarkService;
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
                PublishDate = DateTime.Now,
                Tags = viewModel.Tags,
                Views = 0,
                Image = viewModel.Image,
                UserId = userId,
                User = user,
            };

            await _postService.CreatePostAsync(post); // Assuming _postService is injected

            return RedirectToAction("Index");
       
    }
    
    

    [HttpPost]
    public async Task<IActionResult> BookmarkPost(int postId)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return Json(new { success = false, message = "User not found." });
        }

        var userId = currentUser.Id;
        await _bookmarkService.BookmarkPostbyPostId(userId, postId);
        return Json(new { success = true, message = "Post bookmarked successfully." });
    }

    [HttpGet]
    public async Task<IActionResult> BookmarkedPosts()
    {
        var user = await _userManager.GetUserAsync(User);
        var userId = user.Id;
        var bookmarkedPosts = await _bookmarkService.GetBookmarkedPostsByUserIdAsync(userId);
        return View(bookmarkedPosts);
    }


}