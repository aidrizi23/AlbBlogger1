using System.Drawing.Printing;
using AlbBlogger1.Data;
using AlbBlogger1.Repositories.Pagination;
using Microsoft.EntityFrameworkCore;

namespace AlbBlogger1.Repositories;

public class PostRepository : BaseRepository<Post>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PostRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<PaginatedList<Post>> GetAllPostsByUserId(string userId, int pageIndex = 1, int pageSize = 10)
    {
        var posts = _applicationDbContext.Posts.AsNoTracking().Where(X => X.UserId == userId).AsQueryable();
        return await PaginatedList<Post>.CreateAsync(posts, pageIndex , pageSize );
    }
}