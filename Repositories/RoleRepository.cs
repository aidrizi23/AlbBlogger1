using AlbBlogger1.Data;
using Microsoft.EntityFrameworkCore;

namespace AlbBlogger1.Repositories;

public class RoleRepository : BaseRepository<ApplicationRole>
{
    // first we import the database 
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
     
    public async Task<ApplicationRole> GetRoleByUserIdAsync(string userId)
    {
        return await _context.ApplicationRoles.FirstOrDefaultAsync(x => x.Id == userId);
    }
}