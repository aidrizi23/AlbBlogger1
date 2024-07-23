using AlbBlogger1.Data;
using Microsoft.EntityFrameworkCore;

namespace AlbBlogger1.Repositories;

public class LikeRepository : BaseRepository<Like>
{
    private readonly ApplicationDbContext _context;

    public LikeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<int> LikePostByUserAndPostId(string userId, int postId)
    {
        // Check if the like already exists
        var like = await _context.Likes
            .FirstOrDefaultAsync(x => x.UserId == userId && x.PostId == postId);

        if (like == null)
        {
            // Fetch the related User and Post entities
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Id == userId);
            var post = await _context.Posts
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(x => x.Id == postId);

            if (user == null || post == null)
            {
                // Handle the case where the User or Post does not exist
                throw new Exception("User or Post not found");
            }

            // Create a new Like entity
            like = new Like
            {
                UserId = userId,
                User = user,
                PostId = postId,
                Post = post
            };

            // Add the new Like to the database and to the post's Likes collection
            await _context.Likes.AddAsync(like);
            post.Likes.Add(like);
        }
        else
        {
            // Remove the existing Like
            _context.Likes.Remove(like);
        
            // Remove the like from the post's Likes collection
            var post = await _context.Posts
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(x => x.Id == postId);
            post?.Likes.Remove(like);
        }

        // Save changes to the database
        await _context.SaveChangesAsync();

        // Return the updated like count
        return await GetLikeCountForPost(postId);
    }

    public async Task<IEnumerable<Post>> GetLikedPostsByUserId(string userId)
    {
       return await _context.Likes.Where(X => X.UserId == userId).Select(x => x.Post).ToListAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> GetUsersWhoLikedPostByPostId(int postId)
    {
        return _context.Likes.Where(x => x.PostId == postId).Select(x => x.User);
    }
    public async Task<int> GetLikeCountForPost(int postId)
    {
        return await _context.Likes.CountAsync(l => l.PostId == postId);
    }
}