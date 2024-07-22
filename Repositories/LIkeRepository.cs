using AlbBlogger1.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AlbBlogger1.Repositories;

public class LikeRepository : BaseRepository<Like>
{
    private readonly ApplicationDbContext _context;
    

    public LikeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task LikePostAsync(int postId, string userId)
    {
        var post = await _context.Posts
            .Include(p => p.Likes)
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (post == null)
        {
            throw new Exception("Post not found");
        }

        // Check if the user has already liked the post
        var existingLike = post.Likes.FirstOrDefault(l => l.UserId == userId);
        if (existingLike != null)
        {
            throw new Exception("User has already liked this post");
        }

        post.Likes.Add(new Like { UserId = userId, PostId = postId });

        // Save changes to the database
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetLikesCountAsync(int postId)
    {
        return await _context.Likes.CountAsync(l => l.PostId == postId);
    }

    public async Task<Like> GetByPostAndUserAsync(int postId, string userId)
    {
        return  await _context.Likes.FirstOrDefaultAsync(x => x.PostId == postId &&  x.UserId == userId);
    }
}