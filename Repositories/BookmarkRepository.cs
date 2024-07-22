using AlbBlogger1.Data;
using Microsoft.EntityFrameworkCore;

namespace AlbBlogger1.Repositories;

public class BookmarkRepository : BaseRepository<Bookmark>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public BookmarkRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task BookmarkPostbyPostId(string userId, int postId)
    {
        var bookmark = await _applicationDbContext.Bookmarks
            .Include(b => b.BookmarkedPosts)
            .FirstOrDefaultAsync(x => x.UserId == userId);
    
        if (bookmark == null)
        {
            bookmark = new Bookmark()
            {
                UserId = userId,
                BookmarkedPosts = new List<Post>()
            };
            await _applicationDbContext.Bookmarks.AddAsync(bookmark);
        }

        var post = await _applicationDbContext.Posts.FindAsync(postId);
      

        if (!bookmark.BookmarkedPosts.Any(p => p.Id == post.Id))
        {
            bookmark.BookmarkedPosts.Add(post);
            await _applicationDbContext.SaveChangesAsync();
        }
        else
        {
            bookmark.BookmarkedPosts.Remove(post);
            _applicationDbContext.Bookmarks.Update(bookmark);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
    public async Task<Bookmark> GetBookmarkByUserId(string userId)
    {
        return  await _applicationDbContext.Bookmarks.FirstOrDefaultAsync(x => x.UserId == userId);
    }
    
    public async Task<List<Post>> GetBookmarkedPostsByUserIdAsync(string userId)
    {
        var bookmark = await _applicationDbContext.Bookmarks
            .Include(b => b.BookmarkedPosts)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        return bookmark?.BookmarkedPosts.ToList() ?? new List<Post>();
    }


}