

using AlbBlogger1.Data;
using AlbBlogger1.Repositories.Pagination;

namespace AlbBlogger1.Areas
{
    public interface IUserRepository
    {
        Task<PaginatedList<ApplicationUser>> GetPaginatedUsersAsync(int page = 1, int pagesize = 10);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> FindAllAsync();
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(ApplicationUser user);
        Task SaveChangesAsync();
        //Task<Agent> GetAgentByUserIdAsync(string id);
    }
}