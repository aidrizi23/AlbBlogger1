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

    public async Task LikePostAsync(int postId, string? userId)
    {
        var like = new Like()
        {
            UserId = userId,
            PostId = postId
        };
        await _context.Likes.AddAsync(like);
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