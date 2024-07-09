using System.Drawing.Printing;
using AlbBlogger1.Data;
using AlbBlogger1.Repositories.Pagination;
using AlbBlogger1.Services;
using Microsoft.EntityFrameworkCore;

namespace AlbBlogger1.Repositories;

public class PostRepository : BaseRepository<Post>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ILikeService _likeService;
    public PostRepository(ApplicationDbContext applicationDbContext, ILikeService likeService) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _likeService = likeService;
    }

    public async Task<PaginatedList<Post>> GetAllPaginatedPostsByUserId(string userId, int pageIndex = 1, int pageSize = 10)
    {
        var posts = _applicationDbContext.Posts.AsNoTracking().Where(X => X.UserId == userId).AsQueryable();
        return await PaginatedList<Post>.CreateAsync(posts, pageIndex , pageSize );
    }

    public async Task LikePostByIdAsync(int postId, string userId)
    {
        await _likeService.LikePostAsync(postId, userId);
    }

    public async Task ViewPostByIdAsync(int id)
    {
        var post = await _applicationDbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
        if (post != null)
        {
            post.Views++;
            _applicationDbContext.Posts.Update(post);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
    
    public async Task<PaginatedList<Post>> GetAllPaginatedPostAsync(int pageIndex = 1, int pageSize = 10)
    {
        var posts =  _applicationDbContext.Posts.AsQueryable();
        return await PaginatedList<Post>.CreateAsync(posts, pageIndex, pageSize);
    }
    
    // ------------------------------ Filters ----------------------------
    // public async Task<PaginatedList<Post>> GetPaginatedPostsByHighestLikes(int pageIndex = 1, int pageSize = 10)
    // {
    //     var posts = _applicationDbContext.Posts.AsNoTracking().OrderByDescending(x => x.Likes).AsQueryable();
    //     return await PaginatedList<Post>.CreateAsync(posts, pageIndex, pageSize);
    // }
    //
    // public async Task<PaginatedList<Post>> GetPaginatedPostsByLowestLikes(int pageIndex = 1, int pageSize = 10)
    // {
    //     var posts = _applicationDbContext.Posts.AsNoTracking().OrderBy(x => x.Likes).AsQueryable();
    //     return await PaginatedList<Post>.CreateAsync(posts, pageIndex, pageSize);
    // }
    //
    public async Task<PaginatedList<Post>> GetPaginatedPostsByHighestViews(int pageIndex = 1, int pageSize = 10)
    {
        var posts = _applicationDbContext.Posts.AsNoTracking().OrderByDescending(x => x.Views).AsQueryable();
        return await PaginatedList<Post>.CreateAsync(posts, pageIndex, pageSize);
    }

    public async Task<PaginatedList<Post>> GetPaginatedPostsByLowestViews(int pageIndex = 1, int pageSize = 10)
    {
        var posts = _applicationDbContext.Posts.AsNoTracking().OrderBy(x => x.Views).AsQueryable();
        return await PaginatedList<Post>.CreateAsync(posts, pageIndex, pageSize);
    }
    // -------------------------------------------------------------------------

}