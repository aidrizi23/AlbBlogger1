using AlbBlogger1.Data;
using AlbBlogger1.Migrations;
using AlbBlogger1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Services.Extensions;

namespace AlbBlogger1.Controllers;

public class ReplyController : Controller
{
    private readonly IPostService _postService;
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBookmarkService _bookmarkService;
    private readonly ILikeService _likeService;
    private readonly IReplyService _replyService; // Add this line
    public readonly IFileHandlerService _fileHandleService;

    public ReplyController(IPostService postService, IUserService userService, UserManager<ApplicationUser> userManager,
        IBookmarkService bookmarkService, ILikeService likeService, IReplyService replyService,
        IFileHandlerService fileHandleService)
    {
        _postService = postService;
        _userService = userService;
        _userManager = userManager;
        _bookmarkService = bookmarkService;
        _likeService = likeService;
        _replyService = replyService; // Add this line
        _fileHandleService = fileHandleService;
    }

    
}