
using System.Security.AccessControl;
using AlbBlogger1.Data;
using AlbBlogger1.Models;
using AlbBlogger1.Repositories.Pagination;
using AlbBlogger1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Services.Extensions;

namespace AlbBlogger1.Controllers;

public class PostController : Controller
{
    private readonly IPostService _postService;
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBookmarkService _bookmarkService;
    private readonly ILikeService _likeService;
    private readonly IReplyService _replyService; // Add this line
    public readonly IFileHandlerService _fileHandleService;

    public PostController(IPostService postService, IUserService userService, UserManager<ApplicationUser> userManager, 
        IBookmarkService bookmarkService, ILikeService likeService, IReplyService replyService, IFileHandlerService fileHandleService) // Add replyService parameter
    {
        _postService = postService;
        _userService = userService;
        _userManager = userManager;
        _bookmarkService = bookmarkService;
        _likeService = likeService;
        _replyService = replyService; // Add this line
        _fileHandleService = fileHandleService;
    }

    public async Task<IActionResult> Index(int pageIndex = 1)
    {
        const int pageSize = 10;
        var posts = await _postService.GetAllPaginatedPostsAsync(pageIndex, pageSize);
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

        var replies = await _replyService.GetRepliesByPostIdAsync(id);
        var viewModel = new PostDetailsViewModel
        {
            Post = post,
            Replies = replies
        };

        return View(viewModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateReply(int postId, string content)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var reply = new Reply
        {
            Content = content,
            CreatedAt = DateTime.UtcNow,
            UserId = user.Id,
            PostId = postId
        };

        await _replyService.CreatReplyAsync(reply);

        return RedirectToAction(nameof(Details), new { id = postId });
    }
    
    // [HttpPost]
    // public async Task<IActionResult> CreateReplyForReply(int parentReplyId, int postId, string content)
    // {
    //     var user = await _userManager.GetUserAsync(User);
    //     if (user == null)
    //     {
    //         return Unauthorized();
    //     }
    //
    //     var reply = new Reply
    //     {
    //         Content = content,
    //         CreatedAt = DateTime.UtcNow,
    //         UserId = user.Id,
    //         PostId = postId,
    //         ParentReplyId = parentReplyId
    //     };
    //
    //     await _replyService.CreatReplyAsync(reply);
    //
    //     return RedirectToAction("Details", new { id = postId });
    // }
    
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> CreateReplyForReply(int parentReplyId, int postId, string content)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Json(new { success = false, message = "User not authenticated." });
        }

        var reply = new Reply
        {
            Content = content,
            CreatedAt = DateTime.UtcNow,
            UserId = user.Id,
            PostId = postId,
            ParentReplyId = parentReplyId
        };

        await _replyService.CreatReplyAsync(reply);

        return Json(new { 
            success = true, 
            replyId = reply.Id,
            userName = user.UserName,
            content = reply.Content
        });
    }
    
    
    [HttpPost, HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
        {
            return Json(new { success = false, message = "Post not found" });
        }

        var images = post.Images;
        foreach (var image in images)
        {
            _fileHandleService.RemoveImageFile("uploads/postImages", image);
        }
        await _postService.DeletePostAsync(post);
    
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Json(new { success = true, message = "Post deleted successfully" });
        }
    
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
        if (userId != null)
        {
            if (user != null)
            {
                var post = new Post()
                {
                    Title = viewModel.Title,
                    Content = viewModel.Content,
                    PublishDate = DateTime.Now,
                    Tags = viewModel.Tags,
                    Views = 0,
                    // Image = viewModel.Image,
                    Images = new List<string>(),
                    UserId = userId,
                    User = user,
                    LikeCount = 0,
                };

                await _postService.CreatePostAsync(post);
                
                var otherFiles = HttpContext.Request.Form.Files.Where(f => f.Name.StartsWith("Images")).ToList();
                if (otherFiles.Count > 0)
                {
                    var uploadDir = "uploads/postImages";
                    var fileCollection = new FormFileCollection();
                    foreach (var file in otherFiles)
                    {
                        fileCollection.Add(file);
                    }
                    var fileNames = await _fileHandleService.UploadAsync(fileCollection, uploadDir);
                    post.Images.AddRange(fileNames);
                    await _postService.EditPostAsync(post); 
                }
            }
           
        }
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

    [HttpPost]
    public async Task<IActionResult> LikePost(int postId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var likeCount = await _likeService.LikePostByUserAndPostId(user.Id, postId);

            return Json(new { success = true, message = "Like status updated successfully.", likeCount = likeCount });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "An error occurred while processing your request." });
        }
    }

    public async Task<IActionResult> UsersWhoLikedPost(int postId)
    {
        var users = await _likeService.GetUsersWhoLikedPostByPostId(postId);
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> IncrementViewCount(int id)
    {
        await _postService.ViewPostByIdAsync(id);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> IncrementClickCount(int id)
    {
        await _postService.IncreaseClickCountByPostId(id);
        return Ok();
    }
}
