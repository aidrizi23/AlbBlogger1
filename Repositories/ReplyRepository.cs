using System.Xml.Linq;
using AlbBlogger1.Data;
using Microsoft.EntityFrameworkCore;

namespace AlbBlogger1.Repositories;

public class ReplyRepository : BaseRepository<Reply>
{

    private readonly ApplicationDbContext _context;

    public ReplyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Reply>> GetRepliesByPostIdAsync(int postId)
    {
        return await _context.Replies
            .Where(r => r.PostId == postId)
            .Include(r => r.User)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }
}